using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TgmTasHelper.Simulation;
using TgmTasHelper.Undoable;

namespace TgmTasHelper
{
    [DataContract]
    public class GameFile
    {
        private static DataContractSerializer m_Dcs = new DataContractSerializer(typeof(GameFile), new DataContractSerializerSettings()
        {
            KnownTypes = new[]
            {
                typeof(GameState),
                typeof(MutableBoard),
                typeof(Tetromino),
                typeof(TgmGameRules),
                typeof(TgmRng),
            },
        });

        [DataMember]
        private List<IGameState> m_States = new List<IGameState>();
        [DataMember]
        private List<GameStep> m_Steps = new List<GameStep>();

        public event EventHandler Changed;

        public IReadOnlyList<IGameState> States { get { return m_States; } }
        public IReadOnlyList<GameStep> Steps { get { return m_Steps; } }

        public GameFile(IGameState initialState)
        {
            m_States.Add(initialState);
        }

        public void Write(Stream stream)
        {
            using (var deflateStream = new GZipStream(stream, CompressionLevel.Optimal, true))
            {
                using (var xmlWriter = XmlWriter.Create(deflateStream, new XmlWriterSettings()
                {
                    CloseOutput = false,
                    Indent = true,
                }))
                {
                    m_Dcs.WriteObject(xmlWriter, this);
                }
            }
        }

        public static GameFile Read(Stream stream)
        {
            using (var deflateStream = new GZipStream(stream, CompressionMode.Decompress, true))
            {
                var r = (GameFile)m_Dcs.ReadObject(deflateStream);
                if (r == null)
                    throw new InvalidDataException();
                return r;
            }
        }

        public IUndoable CreateSelectResult(int index, Solver.Result result)
        {
            EnsureInvariant();
            Ensure(index < m_States.Count);
            Ensure(index <= m_Steps.Count);

            var removedStates = m_States.Skip(index + 1).ToList();
            var removedSteps = m_Steps.Skip(index).ToList();
            Ensure(removedStates.Count == removedSteps.Count);

            return new GenericUndoable(() =>
            {
                EnsureInvariant();
                Ensure(index < m_States.Count);
                Ensure(index <= m_Steps.Count);
                m_States.RemoveRange(index + 1, m_States.Count - index - 1);
                m_Steps.RemoveRange(index, m_Steps.Count - index);
                m_States.Add(result.NextState);
                m_Steps.Add(result.Step);
                EnsureInvariant();
                NotifyChanged();
            },
            () =>
            {
                EnsureInvariant();
                Ensure(m_States.Count == index + 2);
                Ensure(m_Steps.Count == index + 1);
                Ensure(removedStates.Count == removedSteps.Count);
                m_States.RemoveLast();
                m_Steps.RemoveLast();
                m_States.AddRange(removedStates);
                m_Steps.AddRange(removedSteps);
                EnsureInvariant();
                NotifyChanged();
            });
        }

        public void NotifyChanged()
        {
            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }

        private void EnsureInvariant()
        {
            // There must always be N steps and N+1 states:
            Ensure(m_States.Count == m_Steps.Count + 1);
        }

        private void Ensure(bool a)
        {
            if (!a)
                throw new InvalidProgramException();
        }
    }
}

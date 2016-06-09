using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgmTasHelper.Simulation;
using TgmTasHelper.Undoable;

namespace TgmTasHelper
{
    public static class FileOperationFactory
    {
        public static IUndoable SelectResult(FileView fileView, Solver.Result result)
        {
            var file = fileView.File;
            int index = fileView.Index;

            Debug.Assert(index < file.States.Count);
            Debug.Assert(index <= file.Steps.Count);

            var removedStates = file.States.Skip(index + 1).ToList();
            var removedSteps = file.Steps.Skip(index).ToList();

            var r = new GenericUndoable(() =>
            {
                file.States.RemoveRange(index + 1, file.States.Count - index - 1);
                file.Steps.RemoveRange(index, file.Steps.Count - index);
                file.States.Add(result.NextState);
                file.Steps.Add(result.Step);
                fileView.Index = index + 1;
            }, () =>
            {
                file.Steps.Remove(result.Step);
                file.States.Remove(result.NextState);
                file.Steps.AddRange(removedSteps);
                file.States.AddRange(removedStates);
                fileView.Index = index;
            });
            return r;
        }
    }
}

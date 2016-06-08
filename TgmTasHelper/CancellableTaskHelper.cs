using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TgmTasHelper
{
    public class CancellableTaskHelper
    {
        private CancellationTokenSource m_CancelTokenSource;

        public async void Start(Func<CancellationToken, Task> action)
        {
            Reset();
            m_CancelTokenSource = new CancellationTokenSource(); ;

            try
            {
                await action(m_CancelTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // ignore...
            }
        }

        public void Reset()
        {
            if (m_CancelTokenSource != null)
            {
                m_CancelTokenSource.Cancel();
                m_CancelTokenSource = null;
            }
        }
    }
}

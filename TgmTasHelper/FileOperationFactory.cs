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
            var index = fileView.Index;
            var fileAction = fileView.File.CreateSelectResult(index, result);

            var r = new GenericUndoable(() =>
            {
                fileView.SuspendUpdates();
                fileAction.Do();
                fileView.Index = index + 1;
                fileView.ResumeUpdates();
            }, () =>
            {
                fileView.SuspendUpdates();
                fileAction.Undo();
                fileView.Index = index;
                fileView.ResumeUpdates();
            });
            return r;
        }
    }
}

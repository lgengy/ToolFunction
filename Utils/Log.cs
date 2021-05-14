using friInterfaceTraceRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFunction.Utils
{
    class Log
    {
        public static CInterfaceTraceRecord TraceRecord = new CInterfaceTraceRecord();
        private static bool InitialTraceFlag = false;

        public Log(string logPath)
        {
            TraceRecord.pComponentName = "ToolFunction";
            TraceRecord.pTraceFileName = "ToolFunction";
            TraceRecord.pTracePath = logPath;
            TraceRecord.pErrFileName = "ToolFunctionERROR";
            TraceRecord.pErrPath = logPath;
            TraceRecord.pMaxFileCount = 50;
        }

        public void WriteTraceInfor(string Infor)
        {
            try
            {
                if (!InitialTraceFlag)
                {
                    InitialTraceFlag = true;
                    TraceRecord.fInitialize();
                }
                TraceRecord.fWriteTrace(Infor);
            }
            catch (Exception)
            {

            }
        }

        public void WriteErrorInfor(string Infor)
        {
            try
            {
                if (!InitialTraceFlag)
                {
                    InitialTraceFlag = true;
                    TraceRecord.fInitialize();
                }
                TraceRecord.fWriteError(Infor);
            }
            catch (Exception)
            {

            }
        }
    }
}

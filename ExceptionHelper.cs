using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Rocket
{
    public class ExceptionHelper
    {
        public static int LineNumber(Exception ex)
        {
            int lineNumber = 0;
            const string lineSearch = ":line ";
            if (ex.StackTrace != null)
            {
                var index = ex.StackTrace.LastIndexOf(lineSearch, StringComparison.Ordinal);
                if (index != -1)
                {
                    int.TryParse(ex.StackTrace.Substring(index + lineSearch.Length), out lineNumber);
                }
            }

            return lineNumber;
        }

        public static List<Rocket.DTO.ErrorLogDto> ErrorList(Exception ex)
        {
            List<Rocket.DTO.ErrorLogDto> errorLogList = new List<Rocket.DTO.ErrorLogDto>();

            try
            {
                string methodName = null;
                string errorLineMethodName = null;
                int lineNumber = Rocket.ExceptionHelper.LineNumber(ex);

                StackFrame[] stackFrameList = new StackTrace(ex, true).GetFrames();
                if (stackFrameList != null && stackFrameList.Length > 1)
                {
                    MethodBase method = stackFrameList[1].GetMethod();

                    if (method != null)
                    {
                        methodName = method.Name;
                        if (method.DeclaringType != null)
                        {
                            methodName = method.DeclaringType.FullName + "." + methodName;
                        }
                    }

                    method = stackFrameList[0].GetMethod();


                    if (method != null)
                    {
                        errorLineMethodName = method.Name;
                        if (method.DeclaringType != null)
                        {
                            errorLineMethodName = method.DeclaringType.FullName + "." + errorLineMethodName;
                        }
                    }
                }
                
                while (ex != null)
                {
                    Guid? parentGUID = null;
                    if (errorLogList.Count > 0)
                    {
                        parentGUID = errorLogList[errorLogList.Count - 1].ErrorLogGUID;
                    }

                    Rocket.DTO.ErrorLogDto errorLog = new Rocket.DTO.ErrorLogDto()
                    {
                        ErrorLogGUID = Guid.NewGuid(),
                        ErrorLogParentGUID = parentGUID,
                        ErrorLogStackTrace = ex.StackTrace,
                        ErrorLogDate = DateTime.Now,
                        //ErrorLogHResult = ex.HResult,
                        ErrorLogHelpLink = ex.HelpLink,
                        ErrorLogMessage = ex.Message,
                        ErrorLogSource = ex.Source,
                        ErrorLogMethod = methodName,
                        ErrorLogLineNumber = lineNumber,
                        ErrorLogLineMethod = errorLineMethodName
                    };

                    errorLogList.Add(errorLog);
                    ex = ex.InnerException;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return errorLogList;
        }
    }
}
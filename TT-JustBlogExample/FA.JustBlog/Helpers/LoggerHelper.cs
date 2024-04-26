using log4net;

namespace FA.JustBlog.Helpers {
    public class LoggerHelper {
        private readonly ILog _log;

        public LoggerHelper(ILog log) {
            _log = log;
        }

        public void LogDebug(string message) {
            _log.Debug(message);
        }

        public void LogInformation(string message) {
            _log.Info(message);
        }

        public void LogWarning(string message) {
            _log.Warn(message);
        }

        public void LogError(string message) {
            _log.Error(message);
        }

        public void LogFatal(string message) {
            _log.Fatal(message);
        }
    }
}

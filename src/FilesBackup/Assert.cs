using System;

namespace FilesBackup
{
    internal class Assert
    {
        public static void NotNull(object parameter, string parameterName)
        {
            if (parameter == null)
                throw new ArgumentNullException(parameterName);
        }
    }
}

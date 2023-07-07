## Getting Started
1. Enter your SMPT CREDENTIALS in `EmailSettings.cs`
   ```c#
        public const string Host = "";
        public const int Port = 587;
        public const string Username = "";
        public const string Password = "";
        public const string Email = "";
   ```
2. Enter your connection string in `appsettings.json` with MultipleActiveResultSets=True
   ```json
     "ConnectionStrings": {
    "Default": ""
          }
   ```

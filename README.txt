1. Please Create A database called Causes on Microsoft SQL Server
2. Open the project Causes.UI.Web
3. Open the Web.Config file
4. On this line under connection Strings
<add name="AppDbContext" connectionString="data source=SIRLAM-PC; initial catalog = Causes; User ID = sa; Password = s.lanre(01); MultipleActiveResultSets = True; App = EntityFramework" providerName="System.Data.SqlClient" />

Change data source from SIRLAM-PC to your SQL server instance or use localhost, leave initial catalog, change User ID to your database user id, change Password to your database password.
5. Run the file Causes.sql to create tables
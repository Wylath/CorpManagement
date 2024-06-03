## Introduction

Corp Management is a project of management in Company.

## Requirements

+ Platform: Windows (x64)
+ SqlServer >= 2014
+ MS Visual Studio >= 15 (2017)

### Optional :

+ Microsoft Visual Studio Installer Projects in Visual Studio

## Getting Started

### Guide for the database

+ The folder "sql/base" content the requests for create the database : "CreateDB.sql"
+ The folder "sql/old" content the requests for the full data in database : "TDB_xxxx_xx_xx.sql"
+ The folder "sql/update" content the last requests for update the database.

### Installation & configuration

Open the file "App.config" in "CorpManagement" and define the different value for the access to database :

```
server : access route for the database (example : "22.22.22.222, 1433", ip and port)
database : DB name
uid : login app access
password
```

### Optional :

```
domain : not used
```

Define the directory for save the files for each elements :

```
DirectoryFilesServicing (example : DirectoryFilesServicing="\\22.22.22.222\FileDB\Servicing")
DirectoryFilesVehicle
DirectoryFilesTires
DirectoryFilesInvoice
DirectoryFilesInsurance
DirectoryFilesArticle
DirectoryFilesEquipment
```

## Reporting issues

Issues can be reported via the [Github issue tracker](https://github.com/Wylath/CorpManagement/issues).


## Copyright

Copyright Wylath © 2021-2022


## Authors &amp; Contributors

Wylath

[Files]
Source: PatronLock\bin\Release\PatronLock.exe; DestDir: {app}; Flags: comparetimestamp; 
Source: PatronLock\bin\Release\PatronLock.exe.config; DestDir: {app}; Flags: comparetimestamp; 
Source: PatronTaskManager\bin\Release\PatronTaskManager.exe; DestDir: {app}; Flags: comparetimestamp replacesameversion; 
Source: PatronTaskManager\Microsoft.Win32.TaskScheduler.dll; DestDir: {app}; Flags: comparetimestamp; 
Source: PatronApi\bin\Release\PatronApi.dll; DestDir: {app}; Flags: comparetimestamp; 
Source: ComputerLock\bin\Release\Lock.dll; DestDir: {app}; Flags: comparetimestamp; 

[Icons]
Name: {group}\PatronLock; Filename: {app}\PatronLock.exe; WorkingDir: {app}; Flags: CreateOnlyIfFileExists;

[Setup]
AppCopyright=Copyright (C) 2011 University of Nebraska-Lincoln
AppName=PatronLock
AppVerName=PatronLock 1.0
DefaultDirName={pf}\PatronLock
AllowRootDirectory=true
OutputDir=D:\code\patronlock\PatronLock
SourceDir=D:\code\patronlock\PatronLock\
OutputBaseFilename=patronlock32
AppID={{1C3B9453-BDE7-4ADF-AAC0-37E29D642369}
SolidCompression=true
Compression=lzma2/Max
InternalCompressLevel=Max
AlwaysShowComponentsList=false
AllowNoIcons=true
DefaultGroupName=PatronLock
MinVersion=,5.1.2600
WizardImageFile=compiler:wizmodernimage-IS.bmp
WizardSmallImageFile=compiler:wizmodernsmallimage-IS.bmp
VersionInfoVersion=1.0
VersionInfoCompany=CORS, University of Nebraska-Lincoln
VersionInfoDescription=PatronLock
VersionInfoTextVersion=v1.0
VersionInfoCopyright=Copyright (C) 2011 University of Nebraska-Lincoln
VersionInfoProductName=PatronLock
VersionInfoProductVersion=1.0
UninstallDisplayIcon={app}\PatronLock.exe

[Run]
Filename: {app}\PatronTaskManager.exe; Parameters: create; WorkingDir: {app}; 

[UninstallRun]
Filename: {app}\PatronTaskManager.exe; Parameters: remove; WorkingDir: {app};

[InnoIDE_Settings]
UseRelativePaths=true

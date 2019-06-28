; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{7C2880D4-9C04-4C51-AF4B-DC56EA56EACA}}
AppName=Auditur
AppVerName=Auditur 3.0
AppPublisher=Nicol�s Len
DefaultDirName={pf}\Auditur
DisableDirPage=yes
DefaultGroupName=Auditur
DisableProgramGroupPage=yes
OutputDir=D:\Users\Nicolas\Desktop
OutputBaseFilename=Auditur 3.0 Installer
Compression=zip
SolidCompression=no

[Languages]
Name: spanish; MessagesFile: compiler:Languages\Spanish.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; flags: checkablealone

[Files]
Source: D:\Users\Nicolas\Documents\Trabajo\Auditur\VS2013\git\Auditur\Presentacion\bin\Release\Auditur.exe; DestDir: {app}; Flags: ignoreversion; Permissions: users-full
Source: D:\Users\Nicolas\Documents\Trabajo\Auditur\VS2013\git\Auditur\Presentacion\bin\Release\*; DestDir: {app}; Flags: ignoreversion recursesubdirs createallsubdirs; Permissions: users-full
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: {group}\Auditur; Filename: {app}\Auditur.exe
Name: {group}\{cm:UninstallProgram,Auditur}; Filename: {uninstallexe}
Name: {commondesktop}\Auditur; Filename: {app}\Auditur.exe; Tasks: desktopicon

[Run]
Filename: {app}\Auditur.exe; Description: {cm:LaunchProgram,Auditur}; Flags: nowait skipifsilent

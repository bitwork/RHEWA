-- Host: WIN7MOBDEV01
-- Database: [Herstellerersteichung]
-- Table: [dbo].[ServerVerbindungsprotokoll]
SET IDENTITY_INSERT [dbo].[ServerVerbindungsprotokoll] ON
INSERT INTO [dbo].[ServerVerbindungsprotokoll] ([Aktion],[Computername],[Domain],[ID],[Lizenzschluessel_FK],[Windowsbenutzer],[Zeitstempel]) VALUES (N'Hole alle Eichprozesse',N'RHE-FERTIGUNG9',N'RHEWA',3718,N'669205fa-22e4-4cdd-bec3-8002b7878631',N'RHEWA\B.Strack',N'2014-03-19 09:23:08.470')
INSERT INTO [dbo].[ServerVerbindungsprotokoll] ([Aktion],[Computername],[Domain],[ID],[Lizenzschluessel_FK],[Windowsbenutzer],[Zeitstempel]) VALUES (N'Hole alle Eichprozesse',N'RHE-FERTIGUNG9',N'RHEWA',3719,N'669205fa-22e4-4cdd-bec3-8002b7878631',N'RHEWA\B.Strack',N'2014-03-19 09:23:36.000')
SET IDENTITY_INSERT [dbo].[ServerVerbindungsprotokoll] OFF
-- Host: WIN7MOBDEV01
-- Database: [Herstellerersteichung]
-- Table: [dbo].[ServerLookup_Auswertegeraet]
UPDATE [dbo].[ServerLookup_Auswertegeraet] SET [Deaktiviert]=0,[ErstellDatum]=N'2014-03-19 08:05:42.127' WHERE [ID] = N'2'
UPDATE [dbo].[ServerLookup_Auswertegeraet] SET [Deaktiviert]=0,[ErstellDatum]=N'2014-03-19 08:05:26.650' WHERE [ID] = N'367c2191-3c6b-4a09-8d2d-0820db18639a'
INSERT INTO [dbo].[ServerLookup_Auswertegeraet] ([Bauartzulassung],[BruchteilEichfehlergrenze],[Deaktiviert],[ErstellDatum],[Genauigkeitsklasse],[GrenzwertLastwiderstandMAX],[GrenzwertLastwiderstandMIN],[GrenzwertTemperaturbereichMAX],[GrenzwertTemperaturbereichMIN],[Hersteller],[ID],[KabellaengeQuerschnitt],[MAXAnzahlTeilungswerteEinbereichswaage],[MAXAnzahlTeilungswerteMehrbereichswaage],[Mindesteingangsspannung],[Mindestmesssignal],[Pruefbericht],[Speisespannung],[Typ]) VALUES (N'',N'',1,N'2014-03-18 12:40:01.657',N'i',N'',N'',N'',N'',N'',N'88f05b67-1605-4da9-89a3-249eddddfb8b',N'',N'          ',N'          ',N'',N'',N'',N'',N'')
-- Host: WIN7MOBDEV01
-- Database: [Herstellerersteichung]
-- Table: [dbo].[ServerLookup_Waegezelle]
UPDATE [dbo].[ServerLookup_Waegezelle] SET [Bauartzulassung]=N'T8063	',[ErstellDatum]=N'2014-03-18 11:27:30.190',[Kriechteilungsfaktor]=N'1',[MinTeilungswert]=N'1',[Pruefbericht]=N'TC8062',[RueckkehrVorlastsignal]=N'1' WHERE [ID] = N'2'
INSERT INTO [dbo].[ServerLookup_Waegezelle] ([Bauartzulassung],[BruchteilEichfehlergrenze],[Deaktiviert],[ErstellDatum],[Genauigkeitsklasse],[GrenzwertTemperaturbereichMAX],[GrenzwertTemperaturbereichMIN],[Hersteller],[Hoechsteteilungsfaktor],[ID],[Kriechteilungsfaktor],[MaxAnzahlTeilungswerte],[Mindestvorlast],[MinTeilungswert],[Neu],[Pruefbericht],[Revisionsnummer],[RueckkehrVorlastsignal],[Typ],[Waegezellenkennwert],[WiderstandWaegezelle]) VALUES (N'1',N'2',1,N'2014-03-18 12:37:59.030',N'A',N'800',N'-20',N'7',N'77',N'4f65097c-e7f9-4583-8358-83536fdc2993',N'6',N'4',N'4',N'4',0,N'4',NULL,N'4',N'4',N'4',N'4')
UPDATE [dbo].[ServerLookup_Waegezelle] SET [Deaktiviert]=0,[ErstellDatum]=N'2014-03-19 08:07:31.877' WHERE [ID] = N'56b2a73e-faf5-4ad5-9906-110d36677760'
INSERT INTO [dbo].[ServerLookup_Waegezelle] ([Bauartzulassung],[BruchteilEichfehlergrenze],[Deaktiviert],[ErstellDatum],[Genauigkeitsklasse],[GrenzwertTemperaturbereichMAX],[GrenzwertTemperaturbereichMIN],[Hersteller],[Hoechsteteilungsfaktor],[ID],[Kriechteilungsfaktor],[MaxAnzahlTeilungswerte],[Mindestvorlast],[MinTeilungswert],[Neu],[Pruefbericht],[Revisionsnummer],[RueckkehrVorlastsignal],[Typ],[Waegezellenkennwert],[WiderstandWaegezelle]) VALUES (N'',N'0,7',0,N'2014-03-18 15:03:01.453',N'C',N'40',N'-10',N'Tedea',N'24000',N'e55f258e-9565-4812-bbda-6bb536b13f9e',N'8000',N'6000',N'0',N'',0,N'TC2272 rev 13',NULL,N'',N'3510-C6-kg',N'2',N'400')
-- Host: WIN7MOBDEV01
-- Database: [Herstellerersteichung]
-- Table: [dbo].[ServerEichprozess]
SET IDENTITY_INSERT [dbo].[ServerEichprozess] ON
UPDATE [dbo].[ServerEichprozess] SET [UploadDatum]=N'2014-03-19 09:13:25.190' WHERE [ID] = 11
SET IDENTITY_INSERT [dbo].[ServerEichprozess] OFF

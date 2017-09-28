Es gibt drei Templates für die Webconfig. Analog gibt es diese auch im EichsoftwareClient:

DEBUG muss im Webservice und Client übernommen werden, wenn der Webservice gedebuggt werden muss. Achtung der Webservice sucht hier eine lokale DB Instanz (ggfs anpassen)
DEBUG STRATO muss im Webservice und Client übernommen werden, wenn der Webservice gedebuggt werden muss. Achtung der Webservice arbeitet hier gegen die Livedatenbank. Der Client arbeitet dann mit dem Visualstudio Debug Webservice, der die Daten aber aus der Liveumgebung zieht
STRATO muss für das Deployment in der Liveumgebung genutzt werden


Zum Deployen des Webservices bitte kontrollieren ob in der Web.config die Informatioen eingetragen sind aus der "WEB STRATO.config".
Anschließend Builden und auf Stratoserver (h2223265.stratoserver.net) unter C:\inetpub\wwwroot\HerstellerersteichungWebservice ablegen und einen IIS Reset durchführen.

ACHTUNG: Wenn der Client von den Änderungen betroffen ist (z.b eine neue Funktion vorhanden ist oder Parameter geändert wurden), muss im EichsoftwareClient die ServiceReference aktualisiert werden.
Hierbei ist zu beachten, das die Web.config des Clients auf den korrekten Server eingestellt ist. Sprich: Wenn der Webservice noch nicht deployed ist und am Client noch entwickelt wird, sollte über die Debug Web.config auf den Entwicklungswebservice referenziert sein, bevor die ServiceReference aktualisiert wird



Server Struktur
Die Anwendung hat mittlerweile eine komplexe Struktur angenommen, die im Folgenden veranschaulicht werden soll:

Allgemeine Schnittstellen:
	- Admin Client - Konfiguration und Auswertungen über die Software. Der Datenabgleich zwischen Datenbank 1 und 2 ist hiermit auch möglich (manuell)
	
	- Eich Client - Tool zum Ausführen der Konformititätsprüfungen. Bei einer "RHEWA"-Lizenz zusätzlich das Tool zum Auswerten und korrigieren von Konformitätsprüfungen
	
	- Strato Server: Der Stratoserver hosted über den IIS den Webservice für die Anwendung. Ausserdem ist die FTP Funktionalität aktiviert um die optionalen Anhänge von den Eichungen bereitzustellen. Zusätzlich ist auf dem Strato Server der SQL Server für die Datenbank 1 installiert.
	
	- Webservice: Der Webservice wird immer dann angesprochen, wenn eine Eichung vom Kunden versandt wird oder ein Update eingefordert wird. Bei einer RHEWA Lizenz werden zusätzlich alle eingereichten Eichungen abgerufen. Bei einer RHEWA Lizenz wird von einer Dauerhaften Internetverbindung ausgegangen. Der Webservice prüft bei jeder Aktion zunächst auf eine gültige und noch aktive Lizenz, sonst wird kein Code ausgeführt. Der Webservice ist die primäre Schnittstelle zwischen den Client Anwendungen und den Datenbanken. Der Webservice wird über den IIS auf dem Stratoserver gehosted. Über die Webconfig, können einige Einstellungen wie die Datenbank konfiguration vorgenommen werden


	- Datenbank 1: ist die primäre Datenbank der Anwendung. Alle Eichungen die eingereicht wurden, als auch die Benutzer, Lizenzen etc. werden hier abgelegt. Dies ist die Datenbank aus denen der Webservice die Daten bereitstellt und Daten ausliest. Datenbank 1 liegt auf dem Stratoserver.
	
	- Datenbank 2: Die Datenbank 2 ist die RHEWA Seitige Replika Datenbank. Sie ist identisch aufgebaut und dient dem Backup des Stratoservers. Über den Admin Client kann die Datenbank 2 mit der DB 1 und vice versa abgeglichen werden. Ausserdem ist es über den Admin Client möglich die Datenbank 2 als aktuelle DB zu verwenden, für den Fall das der Stratoserver nicht erreichbar ist. Da die Datenbank 2 bei RHEWA liegt, können normale Benutzer in dieser Zeit nichts hoch laden. Rhewa kann aber Eichungen korrigieren. 
	
	- Lokale SQL Kompakt Datenbank
		Die lokale SQL Kompakt Datenbank ist die Primäre Datenbank für den Anwender. Sie ist inhaltlich sehr nahe an die Server Datenbank 1(2) angelehnt. Alle Änderungen werden in dieser Datenbank gespeichert. Wenn eine Eichung abgeschlossen ist und an RHEWA übermittelt wird, werden die Daten aus der Struktur der lokalen SQL Kompakt in die Struktur des SQL Servers übernommen. Andersherum, wenn die Serverseitigen Eichungen heruntergeladen werden, werden die Server Datensätze so umgewandelt, dass sie in der lokalen SQL Kompakt abgelegt werden können.

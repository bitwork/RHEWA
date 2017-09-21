Es gibt drei Templates für die Webconfig. Analog gibt es diese auch im EichsoftwareClient:

DEBUG muss im Webservice und Client übernommen werden, wenn der Webservice gedebuggt werden muss. Achtung der Webservice sucht hier eine lokale DB Instanz (ggfs anpassen)
DEBUG STRATO muss im Webservice und Client übernommen werden, wenn der Webservice gedebuggt werden muss. Achtung der Webservice arbeitet hier gegen die Livedatenbank. Der Client arbeitet dann mit dem Visualstudio Debug Webservice, der die Daten aber aus der Liveumgebung zieht
STRATO muss für das Deployment in der Liveumgebung genutzt werden


Zum Deployen des Webservices bitte kontrollieren ob in der Web.config die Informatioen eingetragen sind aus der "WEB STRATO.config".
Anschließend Builden und auf Stratoserver (h2223265.stratoserver.net) unter C:\inetpub\wwwroot\HerstellerersteichungWebservice ablegen und einen IIS Reset durchführen.

ACHTUNG: Wenn der Client von den Änderungen betroffen ist (z.b eine neue Funktion vorhanden ist oder Parameter geändert wurden), muss im EichsoftwareClient die ServiceReference aktualisiert werden.
Hierbei ist zu beachten, das die Web.config des Clients auf den korrekten Server eingestellt ist. Sprich: Wenn der Webservice noch nicht deployed ist und am Client noch entwickelt wird, sollte über die Debug Web.config auf den Entwicklungswebservice referenziert sein, bevor die ServiceReference aktualisiert wird
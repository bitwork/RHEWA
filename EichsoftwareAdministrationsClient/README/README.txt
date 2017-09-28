Doku Benutzer anlegen

Die Benutzer der Anwendung kommen aus der DB Tabelle dbo.Benutzer. Benutzer sind Firmen über die dbo.Firmen zugeordnet. Der Gedanke hinter diesen Tabellen ist der Abgleich mit den Stammdaten aus dem ERP System.

In Zusätzlichkeit enthält die Tabelle dbo.ServerLizensierung die eigentlichen Lizenzen, die mit einem Benutzer verknüpft werden.

Um nun einen neuen Benutzer für die Anwendung anzulegen, muss zunächst sicher gestellt sein, dass dieser in der Tabelle dbo.Benutzer angelegt wurde und einer Firma zugeordnet ist. Dies sollte durch einen Import aus dem ERP System von Zeit zu Zeit automatisch passieren. Allerdings ist der Import seit umstellung auf das neue ERP System noch nicht definiert worden.

Wenn Benutzer existieren, kann der Admin Client hergezogen werden. Dort können über die GUI neue Lizenzen angelegt werden. Hierbei gilt es aber folgendes zu Berücksichtigen: In dem Dialog "neue Lizenz anlegen" werden in dem Benutzer Dropdown nur die Benutzer angezeigt, die noch _keine_ Lizenz haben. Wenn die Dropdown also leer ist, sind keine Benutzer in der dbo.Benutzer enthalten, die noch keine Lizenz haben.
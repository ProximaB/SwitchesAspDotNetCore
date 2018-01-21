# SwitchesAspDotNetCore

Power switches! ESP8266/01s, WebApiCore, CodeFirst, Knockout.


1. Dodanie obsługi webSockets przez klienta, który aktualizuje widok. [Done]
2. Dodanie ekranu logowania. [Done]
3. Basic Auth WebApi + Tabela użytkowników. Response dopasowany do użytkownika. //niekoniecznie basic auth, tylko moze zaimplementowac cos w mvc ctrl.
4. Dodanie do switcha możliwości ustawiania SSID i PASSWORD w trybie AccesPOint.
   I domyślnego dodawania switcha do bazy po połączeniu do Serwera.
   Switch name:[NOT_DEFINED], description[], idRoom[] --aby użytkownik mógł zedytować w cliecie.
[Sekwencja W ESP]
   1. ESP client mode. [DAFAULT] Sprawdzenie czy nie ma ssid i password zapisany w pamięci flash.
   Button > 5s
      Led -> 1 /1s
      Led -> 0
   2. ESP AccesPoint mode.
    Po polączeniu sie do esp dostajemy dostęp do strony w której ustawiamy ssid i password naszego wifi.
    Zapis danych do pamięci !flash!.
    Button [SAVE],[GoToClientMode]
   3. Sprawdzenie, czy dany switch o id [Generowane na podstawie MAC, tymczasowo hardcoded] znajduje sie w bazie. GET switch/id ->response
      Jeżeli nie -> dodaj switcha POST switch
      Jeżeli tak -> idź do 1.
 

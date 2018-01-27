# Switches

**Power switches! Smart Lighting Control Systems and more!**

:heavy_check_mark:   Control your switches with every dievice with internet browser.  
:heavy_check_mark:   Create users with specyfic list of swithes or rooms that they are able to control.

:gem: If you're intersted in this project feel free to contribute to development. I'm waiting for you pull request! :blush:  
###TO DO
- [X] Dodanie obsługi webSockets przez klienta, który aktualizuje widok.
- [X] Dodanie ekranu logowania.
- [ ] Basic Auth WebApi + Tabela użytkowników. Response dopasowany do użytkownika. *(niekoniecznie basic auth, tylko moze zaimplementowac cos w mvc ctrl.)*
- [ ] Dodanie do switcha możliwości ustawiania SSID i PASSWORD w trybie AccesPOint.  
- [ ] Domyślne dodawanie switcha do bazy po połączeniu do Serwera.  
*Switch name: [NOT_DEFINED], description[], idRoom[] * aby użytkownik mógł zedytować w cliecie.*

**[Sekwencja W ESP]**
   1. ESP client mode. [DAFAULT] Sprawdzenie czy nie ma ssid i password zapisany w pamięci flash.
      - Button > 5s  
      - Led -> 1 /1s  
      - Led -> 0  
   2. ESP AccesPoint mode.
    Po polączeniu sie do esp dostajemy dostęp do strony w której ustawiamy ssid i password naszego wifi.  
    Zapis danych do pamięci !flash!.  
    Button [SAVE],[GoToClientMode].  
   3. Sprawdzenie, czy dany switch o id [Generowane na podstawie MAC, tymczasowo hardcoded] znajduje sie w bazie. GET switch/id ->response
      - Jeżeli nie -> dodaj switcha POST switch [WS:aktualizacja widoku po stronie clienta]
      - Jeżeli tak -> idź do 1.  
- [ ] Gdyby był jeszcze czas można pomyśleć o UDP broadcasting, Esp  nasłuchiwało by i pobierało odpowiedni adres do WebApi, żeby nie musiał być zahardcodowany.

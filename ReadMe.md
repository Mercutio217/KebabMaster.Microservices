KebabMaster - Mikroserwisy - Dokumentacja

Łukasz Bielenin - nr indeksu: 13890
Adres repozytorium: https://github.com/Mercutio217/KebabMaster.Microservices

1.Instalacja

    Wymagania:
        Zainstalowane .NET SDK na komputerze.
        Microsoft SQL Server / Express (najlepiej z dostępem domenowym użytkownika systemu).

    Obie solucje w projekcie należy skompilować za pomocą polecenia wiersza poleceń dotnet lub również za pomocą IDE, takiego jak Visual Studio, a następnie uruchomić w przeglądarce.
    W obu przypadkach bazy danych powinny zostać automatycznie utworzone - OrdersDb z zawartymi elementami menu oraz UsersDb z domyślnym administratorem.

    Dane administratora:

        Email: testmail@mail.com
        Hasło: bajorSux

2. Autoryzacja:

    Logowanie:
        Aby się zalogować, należy wykonać żądanie HTTP do aplikacji KebabMaster.Orders.Authorization (POST /authorization/login).
        W przypadku prawidłowych danych otrzymamy Bearer Token, który należy używać w kolejnych wywołaniach.

    Rejestracja:
        Aby się zarejestrować, należy wykonać żądanie POST /authorization/register.
        W ciele żądania następuje walidacja adresu email oraz sprawdzenie, czy użytkownik o podanych danych nie istnieje już w bazie.

3. Połączenie z bazą danych:
    Domyślnie w plikach appsettings.json podany jest connection string do domenowej instancji SQL Server. W przypadku innego użytkownika systemu, należy go zastąpić logowaniem za pomocą użytkownika i hasła.

4. Opis endpointów:

    KebabMaster.Authorization

        POST /authorization/login:
        Logowanie do systemu.
        Autoryzacja: Publiczna.
        Zwraca: Token wraz z datą ważności tokenu.
        Możliwe statusy:
            200 w przypadku powodzenia,   
            401 w przypadku błędnych danych logowania,
            500 w przypadku błędu systemowego.
    
        POST /authorization/register:
        Rejestracja do systemu.
        Autoryzacja: Publiczna.
        Zwraca: Status code 204.
        Możliwe statusy:
            204 w przypadku powodzenia,
            400 w przypadku podania błędnych danych (nieprawidłowy email, istniejące dane),
            500 w przypadku błędu systemowego.

        GET /authorization/users:
        Pobranie użytkowników systemu.
        Autoryzacja: Użytkownik z rolą "Admin".
        Zwraca: Status code 200 oraz listę użytkowników.
        Możliwe statusy:
            200 w przypadku powodzenia,
            500 w przypadku błędu systemowego.

        DELETE /authorization/users/{email}:
        Usunięcie użytkownika.
        Autoryzacja: Użytkownik z rolą "Admin".
        Zwraca: Status code 200.
        Możliwe statusy:
            200 w przypadku powodzenia,
            404 w przypadku podania nieistniejącego klucza,
            500 w przypadku błędu systemowego.

    KebabMaster.Orders

        GET /orders:
        Pobranie zamówień z użyciem filtrów.
        Autoryzacja: Użytkownik z rolą "Admin".
        Zwraca: Status code 200 oraz listę zamówień.
        Możliwe statusy:
            200 w przypadku powodzenia,
            401 w przypadku nie zalogowanego użytkownika
            403 w wypadku nie ystarczających uprawnień
            500 w przypadku błędu systemowego.
    
        GET /orders/{id}:
        Pobranie zamówienia o podanym identyfikatorze.
        Autoryzacja: Użytkownik z rolą "Admin".
        Zwraca: Status code 200 oraz zamówienie.
        Możliwe statusy:
            200 w przypadku powodzenia,
            401 w przypadku nie zalogowanego użytkownika
            403 w wypadku nie ystarczających uprawnień
            500 w przypadku błędu systemowego.

        POST /orders:
        Utworzenie nowego zamówienia.
        Autoryzacja: Zalogowany użytkownik.
        Zwraca: Status code 204.
        Możliwe statusy:
            204 w przypadku powodzenia,
            400 w przypadku podania błędnych danych (nieprawidłowy email, istniejące dane),
            401 w przypadku nie zalogowanego użytkownika
            403 w wypadku nie ystarczających uprawnień
            500 w przypadku błędu systemowego.

        PUT /orders:
        Modyfikacja istniejącego zamówienia.
        Autoryzacja: Użytkownik z rolą "Admin".
        Zwraca: Status code 204.
        Możliwe statusy:
            204 w przypadku powodzenia,
            404 w przypadku podania nieistniejącego identyfikatora,
            401 w przypadku nie zalogowanego użytkownika
            403 w wypadku nie ystarczających uprawnień
            400 w przypadku podania błędnych danych (nieprawidłowy email, istniejące dane),
            500 w przypadku błędu systemowego.

        DELETE /orders/{id}:
        Usunięcie istniejącego zamówienia.
        Autoryzacja: Użytkownik z rolą "Admin".
        Zwraca: Status code 204.
        Możliwe statusy:
            204 w przypadku powodzenia,
            404 w przypadku podania nieistniejącego identyfikatora,
            401 w przypadku nie zalogowanego użytkownika
            403 w wypadku nie ystarczających uprawnień
            400 w przypadku podania błędnych danych (nieprawidłowy email, istniejące dane),
            500 w przypadku błędu systemowego.

        GET /menu:
        Pobranie menu z listą produktów.
        Zwraca: Status code 200 oraz listę produktów.
        Możliwe statusy:
            200 w przypadku powodzenia,
            500 w przypadku błędu systemowego.
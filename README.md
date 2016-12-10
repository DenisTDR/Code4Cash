

- am implementat soluția ca web api
- pentru a rula este nevoie de un server iis sau iis express
- pentru a testa/verifica funcționalitățile implementate se poate folosi documentația de api generată cu ajutorul package-ului swagger
	adică accesând hostname/swagger 

- ca bază de date am folosit MySQL cu EntityFramework, deci pentru a funcționa trebuie ca connectionString-ul din web.config să fie valid
- apoi rulând comanda Update-Database în package manager console se va popula baza de date cu entități random

- pentru a putea face request-uri este necesară autentificarea
	- mai exact apelarea ultimului endpoint de pe pagina cu documentația cu unul din conturile user0, user1 ... user6 și parola "parola01"

	- apoi, token-ul primit trebuie setat în partea de sus a paginii pentru a fi utilizat apoi în toate request-urile
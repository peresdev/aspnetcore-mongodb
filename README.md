# PollProject
Projeto que consiste em uma API para manipulação e gerenciamento de enquetes.

# Tecnologias/bibliotecas utilizadas
- ASP.NET Core
- WebApi
- MongoDB
- Newtonsoft.Json
- Mapster
- Docker (opcional)

# Configurar projeto - Com Docker
Para configurar o projeto com docker, basta executar o comando na raíz desse projeto: `docker-compose up -d`
Com isso, será criado uma instância do MongoDB rodando na porta 9500.

# Configurar projeto - Sem Docker
Para configurar o projeto sem o docker, basta instalar o MongoDB Latest Version (https://www.mongodb.com/download-center/community). 
A porta padrão da instalação do mongo é a 27017.
A porta que está configurada por padrão no projeto é a 9500. Para alterar a porta para a 27017 no projeto, basta ir no arquivo appsettings.json ou appsettings.Development.json (dependendo do ambiente) e mudar o parâmetro de conexão (ConnectionString) em MongoConnection.

# Banco de dados - MongoDB
1. Criar base de dados
```
use polldb
```

Caso crie a base com outro nome, basta ir no arquivo appsettings.json ou appsettings.Development.json (dependendo do ambiente) e mudar o parâmetro de conexão (Database) em MongoConnection.

2. Inserir documentos e confirmar criação base de dados
```
db.getCollection('Poll').insertMany([
{
	"poll_id": NumberLong(1),
	"poll_description": "This is the question",
	"options": [{
			"option_id": NumberLong(1),
			"option_description": "First option"
		},
		{
			"option_id": NumberLong(2),
			"option_description": "Second option"
		},
		{
			"option_id": NumberLong(3),
			"option_description": "Third option"
		}
	]
},

{
	"poll_id": NumberLong(2),
	"poll_description": "This is the question 2",
	"options": [{
			"option_id": NumberLong(4),
			"option_description": "First option 2"
		},
		{
			"option_id": NumberLong(5),
			"option_description": "Second option 2"
		},
		{
			"option_id": NumberLong(6),
			"option_description": "Third option 2"
		}
	]
},

{
	"poll_id": NumberLong(3),
	"poll_description": "This is the question 3",
	"options": [{
			"option_id": NumberLong(7),
			"option_description": "First option 3"
		},
		{
			"option_id": NumberLong(8),
			"option_description": "Second option 3"
		},
		{
			"option_id": NumberLong(9),
			"option_description": "Third option 3"
		}
	]
}
])
```

# Consumir API

Swagger: https://localhost:5001/swagger

## GET - api/poll/:id
Retorna as informações sobre a enquete.

## POST (form-data) - api/poll
Cria uma nova enquete com as suas respectivas opções.

- Estrutura do POST
```
 {
 	"poll_description": "This is the question".
	 "options": ["First Option", "Second Option", "Third Option"]
 }
 ```

Bulk Edit (Postman)

options:First Option
options:Second Option
poll_description:This is the question

## POST api/poll/:id/vote
Registra o voto em uma opção.

- Estrutura do POST
```
 {
 	"option_id": 1
 }
```

## GET api/poll/:id/stats
Retorna estatísicas sobre a enquete, como views e quantidade de votos.

# Autor
Leandro Peres Gonçalves
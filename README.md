# DesafioMesha

Instruções para execução do backend

Para executar o backend, abra o visual studio e vá em Tools >> Nuget Package Manager >> Package Manager Console

E execute o comando Update-Database para criar o banco de dados no servidor sqlserver local.

Após isso, basta executar o projeto API.


Instruções para execução do frontend

Para executar o frontend, acesse o arquivo base.services.ts e adicione a url que o seu backend está executando em protected urlApi = 'SUA URL AQUI /api/'

Após isso, execute o terminal dentro do diretório e use o comando npm i.

Agora é só executar o comando ng s.

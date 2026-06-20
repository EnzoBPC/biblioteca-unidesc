# Relatório

Este relatório apresenta um resumo de como a nossa base tecnológica está funcionando, explicando a nossa estratégia com a **Injeção de Dependência**, o uso do **Entity Framework Core** e onde o banco **SQLite** vai começar a gargalar.

## 1. O Motor do ASP.NET (Injeção de Dependência)

A **Injeção de Dependência (DI)** serve basicamente para evitar que o nosso código fique todo amarrado. Em vez de o nosso Controller ter que "criar" a conexão com o banco de dados na mão toda vez, o próprio ASP.NET cuida disso e já entrega a conexão pronta para a gente usar. Isso deixa o código mais limpo e muito mais fácil de testar.

Lá no arquivo de configuração, a gente define quanto tempo essa "ajuda" do ASP.NET vai durar. Tem três ciclos:
* **Transient:** Cria uma ferramenta nova do zero toda vez que alguém pede no código.
* **Scoped:** Cria uma única vez por clique/requisição do usuário. Ou seja, durante aquela ação, tudo no código usa a mesma instância.
* **Singleton:** Cria uma única vez quando o sistema liga e divide ela com todo mundo para sempre.

O nosso **DbContext** (que é a ponte pro banco de dados) fica registrado como **Scoped**. Ele não pode ser Singleton de jeito nenhum porque ele não sabe lidar com várias pessoas usando ao mesmo tempo (ele não é **"thread-safe"**). Se fosse Singleton, imagina a bagunça: todo mundo tentando passar pela mesma "porta" de conexão na mesma hora. Ia dar fila, travar tudo e até corromper os dados da biblioteca.

## 2. A Mágica do Banco de Dados (EF Core e ORM)

O **Entity Framework Core** atua como o nosso **ORM**. Na prática, ele é um tradutor: a gente escreve o código normal em C# e ele converte isso sozinho para os comandos do banco de dados. A maior vantagem disso é a **produtividade**, já que a gente não precisa ficar perdendo tempo escrevendo script SQL na mão pra fazer o básico de salvar e listar coisas.

A gente trabalha no esquema **Code-First**. Ou seja, o código é que manda na estrutura. Nós montamos as nossas classes (Livro, Reserva) e o próprio EF Core se vira para criar as tabelas no banco seguindo esse molde.

Pra ele não se perder no que já foi criado e no que é novidade, usamos as **Migrations**. É tipo um histórico. Quando rodamos o comando para atualizar o banco, o EF Core olha uma tabela escondida dele chamada `__EFMigrationsHistory` e compara com os arquivos do nosso projeto. Assim, ele sabe exatamente o que mudou e só aplica a diferença.

## 3. Os Limites do SQLite e Escalabilidade

Hoje estamos salvando tudo no **SQLite**. Pra nossa fase de desenvolvimento na máquina local, ele é perfeito pela **portabilidade**: o banco inteiro é só um arquivo `.db` e a gente não precisa instalar e configurar nenhum servidor pesado.

Mas o SQLite tem um ponto fraco sério: ele faz o **bloqueio do arquivo inteiro (file-level locking)**. Se alguém for salvar uma reserva, o SQLite tranca o arquivo `.db` todo. Num cenário com 10.000 acessos simultâneos como a diretoria quer, isso ia formar uma fila gigantesca e a maioria do pessoal ia tomar erro de *timeout* na cara porque o banco estava "ocupado".

A recomendação técnica é largar o SQLite assim que a gente sair dessa fase de testes. Quando o projeto for para **Produção** na nuvem, a gente precisa migrar pra um banco robusto de verdade, tipo **PostgreSQL** ou **SQL Server**. Esses caras aguentam o tranco porque trancam só a linha exata que está sendo alterada (**row-level locking**), deixando o resto do banco livre pra milhares de pessoas usarem ao mesmo tempo sem travar.
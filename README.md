# Documentação da API - Gerenciador de Produtos
 - **Obs: Se for testar trocar a connectionString no appsettings.json**


## Visão Geral

A API Gerenciador de Produtos permite a gestão de produtos, com funcionalidades como cadastro, consulta, atualização de estoque e exclusão de produtos. A aplicação utiliza autenticação JWT para garantir que as operações sejam realizadas apenas por usuários autorizados.


## Endpoints

### Cadastro de Produto

- **Método**: `POST`
- **Rota**: `/api/Produtos/CadastrandoProduto`
- **Descrição**: Cadastra um novo produto no sistema. Apenas usuários com cargos de `Funcionario` ou `Gerente` podem realizar esta ação.
- **Autenticação**: `Bearer token` (usuários autenticados)
- **Body (Exemplo)**:

- **json**
{
  "nome": "Produto Exemplo",
  "descricao": "Descrição do produto",
  "status": "Em estoque",
  "preco": 99.99,
  "quantidadeEmEstoque": 10
}

##### Respostas:
- **201 Created**: Produto cadastrado com sucesso.
- **400 Bad Request**: Dados inválidos (ex: nome vazio, preço menor ou igual a zero).
- **401 Unauthorized**: Usuário não autorizado.


### Consulta de Todos os Produtos
- **Método**: `GET`
- **Rota**: `/api/Produtos/ConsultarTodosProdutos`
- **Descrição**: Recupera todos os produtos cadastrados no sistema. Esta rota está disponível para todos os usuários, sem - necessidade de autenticação.
- **Autenticação**: Não é necessária. 


##### Resposta (Exemplo):
- **json**
[
  {
    "id": 1,
    "nome": "Produto A",
    "descricao": "Descrição do Produto A",
    "status": "Em estoque",
    "preco": 50.00,
    "quantidadeEmEstoque": 20
  },
  {
    "id": 2,
    "nome": "Produto B",
    "descricao": "Descrição do Produto B",
    "status": "Indisponível",
    "preco": 30.00,
    "quantidadeEmEstoque": 0
  }
]


### Consulta de Produtos em Estoque
- **Método**: `GET`
- **Rota**: `/api/Produtos/ConsultarProdutosEmEstoque`
- **Descrição**: Recupera todos os produtos que estão disponíveis em estoque. Esta rota também está disponível para todos os usuários.
- **Autenticação**: Não é necessária.


#####  Resposta (Exemplo):
 - **json**
[
  {
    "id": 1,
    "nome": "Produto A",
    "descricao": "Descrição do Produto A",
    "status": "Em estoque",
    "preco": 50.00,
    "quantidadeEmEstoque": 20
  }
]

### Atualizar Estoque de Produto
- **Método**: `PUT`
- **Rota**: `/api/Produtos/AtualizarEstoque/{id}`
- **Descrição**: Atualiza a quantidade em estoque de um produto. Apenas usuários com cargos de Funcionario ou Gerente podem realizar esta ação.
- **Autenticação**: Bearer token (usuários autenticados com Funcionario ou Gerente) 


- **Body (Exemplo)**:
 - **json**
{
  "quantidade": 5
}


#### Respostas:
- **200 OK**: Estoque atualizado com sucesso.
- **400 Bad Request**: Quantidade inválida (ex: número negativo).
- **404 Not Found**: Produto não encontrado.


### Excluir Produto
- **Método**: `DELETE`
- **Rota**: `/api/Produtos/{id}`
- **Descrição**: Exclui um produto do sistema. Apenas usuários com o cargo de Gerente podem realizar esta ação.
- **Autenticação**: Bearer token (usuários autenticados com Gerente)


##### Respostas:
- **200 OK**: Produto excluído com sucesso.
- **404 Not Found**: Produto não encontrado.



### Autenticação Login

- **Método**: `POST`
- **Rota**: `/api/auth/Login`
 

- **Body (Exemplo)**:

- **json**
{
  "cpf": "usuario_exemplo",
  "senha": "senha_do_usuario"
}

##### Resposta (Exemplo):
- **json**
 {
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxMjM0NTY3ODkwIiwiaWF0IjoxNjA5MjY1NjQwfQ.-E_1HgqTGBImW4QY1Vj3hRuJSu5gVoZEKqK_jhjg8XE"
}

### Autenticação Registrar
**Método**: `POST`
- **Rota**: /api/auth/Registrar
- **Body (Exemplo)**:

- **json**
{
  "id": 0,
  "nome": "string",
  "cpf": "string",
  "cargo": 1,
  "senha": "string"
}

##### Resposta (Exemplo):
- **json**
 {
    Usuário registrado com sucesso.
}

#### **Inclua o token recebido no cabeçalho Authorization de suas requisições seguintes**:

 - `Authorization`: **Bearer <seu_token_aqui>**

### Produto
- **json**
{
  "id": 1,
  "nome": "Produto Exemplo",
  "descricao": "Descrição do produto",
  "status": "Em estoque",
  "preco": 99.99,
  "quantidadeEmEstoque": 10
}

#### Usuario
- **json**
{
  "id": 1,
  "nome": "João Silva",
  "cpf": "12345678901",
  "cargo": 1,
  "senha": "senha123"
}

##### **Exemplos de Erros Comuns**
- `401 Unauthorized` : **Quando o token não é fornecido ou é inválido.**
- `400 Bad Request`: **Quando os dados enviados na requisição estão malformados ou incompletos.**
- `404 Not Found`:**Quando um recurso solicitado não é encontrado (por exemplo, produto não encontrado).**
- `403 Forbidden`: **Quando um usuário sem permissões adequadas tenta realizar uma ação restrita.**


### Conclusão
- **Essa API fornece uma forma simples e eficaz de gerenciar produtos em um sistema, com controle de acesso baseado em roles (cargos) para garantir que apenas usuários autorizados possam realizar operações sensíveis, como a exclusão de produtos ou a alteração de estoques. A autenticação baseada em JWT oferece segurança adicional para garantir que apenas usuários autenticados possam interagir com a API de forma protegida.**



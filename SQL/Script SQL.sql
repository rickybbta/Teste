-- Criar Tabela Cliente com AUTO_INCREMENT
CREATE TABLE Cliente (
    cliente_id INT AUTO_INCREMENT PRIMARY KEY, -- ID auto-incrementado
    nome VARCHAR(100) NOT NULL, -- Nome do cliente
    cpf CHAR(11) NOT NULL, -- CPF do cliente
    cliente_referencia_id INT NULL, -- Cliente de referência (auto-relacionamento)
    CONSTRAINT FK_Cliente_Referencia FOREIGN KEY (cliente_referencia_id) REFERENCES Cliente(cliente_id) -- Auto-relacionamento
);

-- Criar Tabela Endereco
CREATE TABLE Endereco (
    endereco_id INT AUTO_INCREMENT PRIMARY KEY, -- ID auto-incrementado para endereço
    cliente_id INT NOT NULL, -- FK para Cliente
    tipo_endereco INT NOT NULL CHECK (tipo_endereco IN (1, 2, 3)), -- Tipo de endereço (1 = residencial, 2 = comercial, 3 = cobrança)
    logradouro VARCHAR(150) NOT NULL, -- Logradouro completo
    cidade VARCHAR(60) NOT NULL, -- Cidade
    estado CHAR(2) NOT NULL, -- Sigla do estado
    CONSTRAINT FK_Endereco_Cliente FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id) ON DELETE CASCADE -- FK para Cliente
);

-- Criar Tabela Produto
CREATE TABLE Produto (
    produto_id INT AUTO_INCREMENT PRIMARY KEY, -- ID auto-incrementado para produto
    nome VARCHAR(100) NOT NULL, -- Nome do produto
    preco DECIMAL(12,2) NOT NULL, -- Preço do produto
    codigo VARCHAR(20) NOT NULL -- Código do produto
);

-- Criar Tabela Pedido
CREATE TABLE Pedido (
    pedido_id INT AUTO_INCREMENT PRIMARY KEY, -- ID auto-incrementado para pedido
    data DATE NOT NULL, -- Data do pedido
    cliente_id INT NOT NULL, -- FK para Cliente
    CONSTRAINT FK_Pedido_Cliente FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id) ON DELETE CASCADE -- FK para Cliente
);

-- Criar Tabela Pedido_Produto (Tabela de associação para N:N entre Pedido e Produto)
CREATE TABLE Pedido_Produto (
    pedido_id INT NOT NULL, -- FK para Pedido
    produto_id INT NOT NULL, -- FK para Produto
    quantidade DECIMAL(10,4) NOT NULL, -- Quantidade de produtos
    preco_unitario DECIMAL(12,2) NOT NULL, -- Preço unitário do produto
    PRIMARY KEY (pedido_id, produto_id), -- Chave composta
    CONSTRAINT FK_PedidoProduto_Pedido FOREIGN KEY (pedido_id) REFERENCES Pedido(pedido_id) ON DELETE CASCADE, -- FK para Pedido
    CONSTRAINT FK_PedidoProduto_Produto FOREIGN KEY (produto_id) REFERENCES Produto(produto_id) ON DELETE CASCADE -- FK para Produto
);

-- Criar Tabela Pagamento
CREATE TABLE Pagamento (
    pagamento_id INT AUTO_INCREMENT PRIMARY KEY, -- ID auto-incrementado para pagamento
    pedido_id INT NOT NULL, -- FK para Pedido
    valor DECIMAL(12,2) NOT NULL, -- Valor do pagamento
    metodo_pagamento INT NOT NULL CHECK (metodo_pagamento IN (1, 2, 3)), -- Método de pagamento (1 = dinheiro, 2 = cartão, 3 = pix)
    CONSTRAINT FK_Pagamento_Pedido FOREIGN KEY (pedido_id) REFERENCES Pedido(pedido_id) ON DELETE CASCADE -- FK para Pedido
);

-- Exemplo de script DML para inserir dados com AUTO_INCREMENT

-- Inserindo Cliente
INSERT INTO Cliente (nome, cpf, cliente_referencia_id)
VALUES ('João Silva', '12345678901', NULL);

-- Inserindo Endereco
INSERT INTO Endereco (cliente_id, tipo_endereco, logradouro, cidade, estado)
VALUES (1, 1, 'Rua A, 123', 'São Paulo', 'SP'); -- cliente_id = 1 (Auto Increment gerado)

-- Inserindo Produto
INSERT INTO Produto (nome, preco, codigo)
VALUES ('Produto A', 100.00, 'PROD001');

-- Inserindo Pedido
INSERT INTO Pedido (data, cliente_id)
VALUES (CURDATE(), 1); -- cliente_id = 1

-- Inserindo Pedido_Produto
INSERT INTO Pedido_Produto (pedido_id, produto_id, quantidade, preco_unitario)
VALUES (1, 1, 2, 100.00); -- pedido_id = 1, produto_id = 1

-- Inserindo Pagamento
INSERT INTO Pagamento (pedido_id, valor, metodo_pagamento)
VALUES (1, 200.00, 2); -- pedido_id = 1

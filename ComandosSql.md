```sql
CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    SenhaHash VARBINARY(64) NOT NULL,
    DataNascimento DATE,
    Peso DECIMAL(5,2),
    Altura DECIMAL(5,2)
);

CREATE TABLE Dietas (
    DietaID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao TEXT
);

CREATE TABLE Exercicios (
    ExercicioID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    CaloriasQueima DECIMAL(6,2) NOT NULL
);

CREATE TABLE Alimentos (
    AlimentoID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Calorias DECIMAL(6,2) NOT NULL,
    Proteinas DECIMAL(6,2),
    Carboidratos DECIMAL(6,2),
    Gorduras DECIMAL(6,2)
);

CREATE TABLE UsuarioDieta (
    UsuarioID INT,
    DietaID INT,
    PRIMARY KEY (UsuarioID, DietaID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID) ON DELETE CASCADE,
    FOREIGN KEY (DietaID) REFERENCES Dietas(DietaID) ON DELETE CASCADE
);

CREATE TABLE UsuarioExercicio (
    UsuarioID INT,
    ExercicioID INT,
    DataExecucao DATETIME NOT NULL,
    DuracaoMinutos INT NOT NULL,
    PRIMARY KEY (UsuarioID, ExercicioID, DataExecucao),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID) ON DELETE CASCADE,
    FOREIGN KEY (ExercicioID) REFERENCES Exercicios(ExercicioID) ON DELETE CASCADE
);

CREATE TABLE DietaAlimento (
    DietaID INT,
    AlimentoID INT,
    PRIMARY KEY (DietaID, AlimentoID),
    FOREIGN KEY (DietaID) REFERENCES Dietas(DietaID) ON DELETE CASCADE,
    FOREIGN KEY (AlimentoID) REFERENCES Alimentos(AlimentoID) ON DELETE CASCADE
);

CREATE PROCEDURE SelecionarUsuarios
AS
BEGIN
    SELECT u.UsuarioID, u.Nome, u.Email, d.DietaID, d.Nome AS Dieta, e.ExercicioID, e.Nome AS Exercicio
    FROM Usuarios u
    LEFT JOIN UsuarioDieta ud ON u.UsuarioID = ud.UsuarioID
    LEFT JOIN Dietas d ON ud.DietaID = d.DietaID
    LEFT JOIN UsuarioExercicio ue ON u.UsuarioID = ue.UsuarioID
    LEFT JOIN Exercicios e ON ue.ExercicioID = e.ExercicioID;
END;

CREATE PROCEDURE SelecionarDietas
AS
BEGIN
    SELECT d.DietaID, d.Nome, d.Descricao, a.AlimentoID, a.Nome AS Alimento
    FROM Dietas d
    LEFT JOIN DietaAlimento da ON d.DietaID = da.DietaID
    LEFT JOIN Alimentos a ON da.AlimentoID = a.AlimentoID;
END;

CREATE PROCEDURE SelecionarExercicios
AS
BEGIN
    SELECT * FROM Exercicios;
END;

CREATE PROCEDURE SelecionarAlimentos
AS
BEGIN
    SELECT * FROM Alimentos;
END;

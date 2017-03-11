-- Generado por Oracle SQL Developer Data Modeler 4.1.0.881
--   en:        2017-03-10 19:58:58 CST
--   sitio:      SQL Server 2012
--   tipo:      SQL Server 2012




CREATE
  TABLE CREDITODEBITO
  (
    descripcion VARCHAR (500) ,
    monto FLOAT (10) NOT NULL ,
    fecha DATETIME NOT NULL ,
    tipo BIT NOT NULL ,
    id_cuenta INTEGER NOT NULL
  )
  ON "default"
GO

CREATE
  TABLE CUENTA
  (
    id_cuenta INTEGER NOT NULL ,
    saldo FLOAT (10) NOT NULL ,
    id_usuario INTEGER NOT NULL ,
    CONSTRAINT CUENTA_PK PRIMARY KEY CLUSTERED (id_cuenta)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO

CREATE
  TABLE SERVICIO
  (
    nombre      VARCHAR (30) ,
    descripcion VARCHAR (500) ,
    monto FLOAT (10) NOT NULL ,
    fecha              DATETIME NOT NULL ,
    id_cuenta_origen   INTEGER NOT NULL ,
    id_cuenta_servicio INTEGER NOT NULL
  )
  ON "default"
GO

CREATE
  TABLE TRANSFERENCIA
  (
    monto FLOAT (10) NOT NULL ,
    fecha             DATE NOT NULL ,
    id_cuenta_destino INTEGER NOT NULL ,
    id_cuenta_origen  INTEGER NOT NULL
  )
  ON "default"
GO

CREATE
  TABLE USUARIO
  (
    id_usuario INTEGER NOT NULL ,
    usuario    VARCHAR (30) NOT NULL ,
    correo     VARCHAR (30) NOT NULL ,
    contraseña VARCHAR (30) NOT NULL ,
    nombre     VARCHAR (30) NOT NULL ,
    apellido   VARCHAR (30) NOT NULL ,
    CONSTRAINT USUARIO_PK PRIMARY KEY CLUSTERED (id_usuario)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO

ALTER TABLE CREDITODEBITO
ADD CONSTRAINT CREDITODEBITO_CUENTA_FK FOREIGN KEY
(
id_cuenta
)
REFERENCES CUENTA
(
id_cuenta
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE CUENTA
ADD CONSTRAINT CUENTA_USUARIO_FK FOREIGN KEY
(
id_usuario
)
REFERENCES USUARIO
(
id_usuario
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SERVICIO
ADD CONSTRAINT SERVICIO_CUENTA_ORIGEN_FK FOREIGN KEY
(
id_cuenta_origen
)
REFERENCES CUENTA
(
id_cuenta
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SERVICIO
ADD CONSTRAINT SERVICIO_CUENTA_SERVICIO_FK FOREIGN KEY
(
id_cuenta_servicio
)
REFERENCES CUENTA
(
id_cuenta
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE TRANSFERENCIA
ADD CONSTRAINT TRANSFERENCIA_CUENTA_DESTINO_FK FOREIGN KEY
(
id_cuenta_destino
)
REFERENCES CUENTA
(
id_cuenta
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE TRANSFERENCIA
ADD CONSTRAINT TRANSFERENCIA_CUENTA_ORIGEN_FK FOREIGN KEY
(
id_cuenta_origen
)
REFERENCES CUENTA
(
id_cuenta
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO


-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                             5
-- CREATE INDEX                             0
-- ALTER TABLE                              6
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE SEQUENCE                          0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0

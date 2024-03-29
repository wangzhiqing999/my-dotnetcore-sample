CREATE SCHEMA IF NOT EXISTS my_trading;
CREATE TABLE IF NOT EXISTS my_trading."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE SCHEMA IF NOT EXISTS my_trading;

CREATE TABLE my_trading.simple_trading (
    trading_id bigint GENERATED BY DEFAULT AS IDENTITY,
    trading_item_code character varying(16) NOT NULL,
    trading_quantity integer NOT NULL,
    open_date timestamp without time zone NOT NULL,
    open_price numeric NOT NULL,
    close_date timestamp without time zone NULL,
    close_price numeric NULL,
    CONSTRAINT "PK_simple_trading" PRIMARY KEY (trading_id)
);

INSERT INTO my_trading."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220110063400_MyTradingInit', '5.0.13');

COMMIT;


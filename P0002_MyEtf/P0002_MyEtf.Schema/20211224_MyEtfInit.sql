CREATE SCHEMA IF NOT EXISTS my_etf;
CREATE TABLE IF NOT EXISTS my_etf."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE SCHEMA IF NOT EXISTS my_etf;

CREATE TABLE my_etf.etf_master (
    etf_code character varying(16) NOT NULL,
    etf_name character varying(64) NULL,
    CONSTRAINT "PK_etf_master" PRIMARY KEY (etf_code)
);

CREATE TABLE my_etf.etf_day_line (
    etf_code character varying(16) NOT NULL,
    trading_date timestamp without time zone NOT NULL,
    open_price numeric NOT NULL,
    close_price numeric NOT NULL,
    highest_price numeric NOT NULL,
    lowest_price numeric NOT NULL,
    volume bigint NOT NULL,
    CONSTRAINT "PK_etf_day_line" PRIMARY KEY (etf_code, trading_date),
    CONSTRAINT "FK_etf_day_line_etf_master_etf_code" FOREIGN KEY (etf_code) REFERENCES my_etf.etf_master (etf_code) ON DELETE CASCADE
);

CREATE TABLE my_etf.etf_day_tr (
    etf_code character varying(16) NOT NULL,
    trading_date timestamp without time zone NOT NULL,
    tr numeric NOT NULL,
    atr numeric NOT NULL,
    CONSTRAINT "PK_etf_day_tr" PRIMARY KEY (etf_code, trading_date),
    CONSTRAINT "FK_etf_day_tr_etf_master_etf_code" FOREIGN KEY (etf_code) REFERENCES my_etf.etf_master (etf_code) ON DELETE CASCADE
);

CREATE TABLE my_etf.etf_week_line (
    etf_code character varying(16) NOT NULL,
    trading_date timestamp without time zone NOT NULL,
    open_price numeric NOT NULL,
    close_price numeric NOT NULL,
    highest_price numeric NOT NULL,
    lowest_price numeric NOT NULL,
    volume bigint NOT NULL,
    CONSTRAINT "PK_etf_week_line" PRIMARY KEY (etf_code, trading_date),
    CONSTRAINT "FK_etf_week_line_etf_master_etf_code" FOREIGN KEY (etf_code) REFERENCES my_etf.etf_master (etf_code) ON DELETE CASCADE
);

INSERT INTO my_etf."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211224114401_MyEtfInit', '5.0.13');

COMMIT;


START TRANSACTION;

CREATE TABLE my_etf.etf_day_macd (
    etf_code character varying(16) NOT NULL,
    trading_date timestamp without time zone NOT NULL,
    diff numeric NOT NULL,
    dea numeric NOT NULL,
    macd numeric NOT NULL,
    CONSTRAINT "PK_etf_day_macd" PRIMARY KEY (etf_code, trading_date),
    CONSTRAINT "FK_etf_day_macd_etf_master_etf_code" FOREIGN KEY (etf_code) REFERENCES my_etf.etf_master (etf_code) ON DELETE CASCADE
);

CREATE TABLE my_etf.etf_week_macd (
    etf_code character varying(16) NOT NULL,
    trading_date timestamp without time zone NOT NULL,
    diff numeric NOT NULL,
    dea numeric NOT NULL,
    macd numeric NOT NULL,
    CONSTRAINT "PK_etf_week_macd" PRIMARY KEY (etf_code, trading_date),
    CONSTRAINT "FK_etf_week_macd_etf_master_etf_code" FOREIGN KEY (etf_code) REFERENCES my_etf.etf_master (etf_code) ON DELETE CASCADE
);

INSERT INTO my_etf."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211227051147_MyEtfMacd', '5.0.13');

COMMIT;






START TRANSACTION;

CREATE TABLE my_etf.etf_day_ema (
    etf_code character varying(16) NOT NULL,
    trading_date timestamp without time zone NOT NULL,
    ema12 numeric NOT NULL,
    ema26 numeric NOT NULL,
    CONSTRAINT "PK_etf_day_ema" PRIMARY KEY (etf_code, trading_date),
    CONSTRAINT "FK_etf_day_ema_etf_master_etf_code" FOREIGN KEY (etf_code) REFERENCES my_etf.etf_master (etf_code) ON DELETE CASCADE
);

CREATE TABLE my_etf.etf_week_ema (
    etf_code character varying(16) NOT NULL,
    trading_date timestamp without time zone NOT NULL,
    ema12 numeric NOT NULL,
    ema26 numeric NOT NULL,
    CONSTRAINT "PK_etf_week_ema" PRIMARY KEY (etf_code, trading_date),
    CONSTRAINT "FK_etf_week_ema_etf_master_etf_code" FOREIGN KEY (etf_code) REFERENCES my_etf.etf_master (etf_code) ON DELETE CASCADE
);

INSERT INTO my_etf."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211227114905_MyEtfEma', '5.0.13');

COMMIT;


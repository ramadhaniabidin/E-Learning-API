--USE [E-Learning]
--GO

--/****** Object: Table [dbo].[master_detail_akun] Script Date: 13/06/2024 20.00.00 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[master_akun] (
--    [id]        INT           IDENTITY (1, 1) NOT NULL,
--    --[id_akun]   INT           NULL,
--	[email]		VARCHAR(MAX),
--	[password]	VARCHAR(MAX),
--    [nama]      VARCHAR (100) NULL,
--    [provinsi]  VARCHAR (100) NULL,
--    [kabupaten] VARCHAR (100) NULL,
--    [kecamatan] VARCHAR (100) NULL,
--    [desa]      VARCHAR (MAX) NULL,
--    [no_telp]   VARCHAR (50)  NULL,
--	akun_aktif BIT DEFAULT 0
--);

SELECT * FROM master_akun

SELECT TOP 1 * FROM Provinsi
SELECT TOP 1 * FROM Kabupaten
SELECT TOP 1 * FROM Kecamatan
SELECT TOP 1 * FROM DESA

SELECT p.namaProvinsi, ka.namaKabupaten, ke.namaKecamatan, d.namaDesa, ROW_NUMBER() OVER(ORDER BY p.id ASC) AS pageIndex
FROM Provinsi p
LEFT JOIN Kabupaten ka ON p.id = ka.idProv
LEFT JOIN Kecamatan ke ON ka.id = ke.idKab
LEFT JOIN DESA d ON ke.id = d.idKec
WHERE pageIndex < 10

SELECT 
    namaProvinsi, 
    namaKabupaten, 
    namaKecamatan, 
    namaDesa, 
    pageIndex
FROM (
    SELECT 
        p.namaProvinsi, 
        ka.namaKabupaten, 
        ke.namaKecamatan, 
        d.namaDesa, 
        ROW_NUMBER() OVER (ORDER BY p.id ASC) AS pageIndex
    FROM 
        Provinsi p
    LEFT JOIN 
        Kabupaten ka ON p.id = ka.idProv
    LEFT JOIN 
        Kecamatan ke ON ka.id = ke.idKab
    LEFT JOIN 
        DESA d ON ke.id = d.idKec
) AS SubQuery
WHERE 
    pageIndex < 10;


INSERT INTO master_alamat
(namaProvinsi, namaKabupaten, namaKecamatan, namaDesa)
SELECT p.namaProvinsi, ka.namaKabupaten, ke.namaKecamatan, d.namaDesa
FROM Provinsi p
LEFT JOIN Kabupaten ka ON p.id = ka.idProv
LEFT JOIN Kecamatan ke ON ka.id = ke.idKab
LEFT JOIN DESA d ON ke.id = d.idKec

--CREATE TABLE master_alamat
--(
--    id INT IDENTITY(1,1), namaProvinsi VARCHAR(MAX),
--    namaKabupaten VARCHAR(MAX), namaKecamatan VARCHAR(MAX),
--    namaDesa VARCHAR(MAX)
--)

SELECT COUNT(*) FROM master_alamat
--DROP TABLE master_alamat
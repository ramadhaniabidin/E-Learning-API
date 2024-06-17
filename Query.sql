DECLARE @pageIndex INT = 1
DECLARE @pageSize INT = 10

SELECT *
FROM
(
	SELECT *, ROW_NUMBER() OVER (ORDER BY namaProvinsi ASC) AS rowNum, 
	((SELECT COUNT(*) FROM (
		SELECT * FROM Provinsi WHERE namaProvinsi LIKE '%%'
	) AS SubQuery) / @pageSize) + 1 AS totalPage
	FROM Provinsi WHERE namaProvinsi LIKE '%%'
) 
AS SubQuery
WHERE rowNum > (@pageSize * (@pageIndex - 1)) 
  AND rowNum <= (@pageSize * @pageIndex)
  

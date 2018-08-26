CREATE PROCEDURE usp_PatientDetailsAdd
(
	@Record XML
)
AS
	BEGIN
		SET XACT_ABORT ON
		BEGIN TRAN

		DECLARE @Id INT = 0

			INSERT INTO PatientDetails (
															Record
														  ) 
											 VALUES (
															@Record
														   )
		 SET  @Id = SCOPE_IDENTITY()
		COMMIT
		SELECT @Id
	END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[usp_PatientDetailsAdd] TO PUBLIC
    AS [dbo];


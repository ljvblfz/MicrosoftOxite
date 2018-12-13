IF NOT EXISTS(SELECT * FROM oxite_Blogs_PostViewType WHERE PostViewTypeName = 'Post-Web')
BEGIN
	INSERT INTO
		oxite_Blogs_PostViewType
	(
		PostViewTypeID,
		PostViewTypeName
	)
	VALUES
	(
		'A38FD696-22E2-4612-A392-E73FCAABB61D',
		'Post-Web'
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Blogs_PostViewType WHERE PostViewTypeName = 'Post-RSS')
BEGIN
	INSERT INTO
		oxite_Blogs_PostViewType
	(
		PostViewTypeID,
		PostViewTypeName
	)
	VALUES
	(
		'6A945758-48C3-4e43-A1E3-AED601F5F022',
		'Post-RSS'
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Blogs_PostViewType WHERE PostViewTypeName = 'Post-ATOM')
BEGIN
	INSERT INTO
		oxite_Blogs_PostViewType
	(
		PostViewTypeID,
		PostViewTypeName
	)
	VALUES
	(
		'E3080032-0203-42c4-8B36-76A8168FB474',
		'Post-ATOM'
	)
END
Module Modulo

    Private server = "ADAMO-PC\SQLEXPRESS"
    Private dataBase = "pdv"
    Private user = "sa"
    Private password = "senha"

    Public ReadOnly strCon = $"Data Source={server}; Integrated Security=true; Initial Catalog={dataBase}; User Id={user}; Password={password}"

End Module

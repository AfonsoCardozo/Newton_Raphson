Public Class frm_principal
    Private Sub btn_calc_Click(sender As Object, e As EventArgs) Handles btn_calc.Click
        Dim xini, x, Fx, aux_Fx, aux_Fdx, Fdx, nE As Decimal
        Dim itera, qntd_x, tam, i, j, t, cont_n As Integer
        Dim part(20), aux_part, sem_x(20) As Integer
        Dim func, numeros(2, 10, 10), num_temp As String
        Dim exp(20) As Decimal

        cont_n = 1 : j = 1
        xini = 0 : x = 0 : Fx = 0 : aux_Fx = 0 : aux_Fdx = 0 : Fdx = 0 : nE = 0
        itera = 0 : qntd_x = 0 : tam = 0 : i = 0 : j = 1 : t = 0 : cont_n = 1
        num_temp = ""

        listIteracao.Items.Clear()

        func = Replace(txb_func.Text, "X", "x")
        x = CDec(Replace(mtxt_x.Text, ".", ","))
        nE = CDec(mtxt_erro.Text)

        tam = Len(func)

        For i = 1 To tam Step 1
            Debug.Print(Mid(func, i, 1))
            If (IsNumeric(Mid(func, i, 1))) Then
                num_temp = num_temp & Mid(func, i, 1)
                Debug.Print(num_temp)
                t = i
                While (IsNumeric(Mid(func, t + 1, 1)))
                    num_temp = num_temp & Mid(func, i + 1, 1)
                    Debug.Print(num_temp)
                    t += 1
                    i += 1
                    cont_n += 1
                End While

                If (Mid(func, i + 1, 1) = "x") Then
                    numeros(1, j, 2) = numeros(1, j, 2) & num_temp
                    num_temp = ""
                    part(j) = CDec(numeros(1, j, 2))
                End If

                If (Mid(func, i - 1, 1) = "^") Then
                    numeros(2, j, 2) = numeros(2, j, 2) & num_temp
                    num_temp = ""
                    exp(j) = CDec(numeros(2, j, 2))
                End If
                If (Mid(func, i + 1, 1) <> "x" And Mid(func, i - 1, 1) <> "^") Then
                    numeros(1, j, 2) = numeros(1, j, 2) & num_temp
                    num_temp = ""
                    part(j) = CDec(numeros(1, j, 2))
                    sem_x(j) = 1
                End If
                If (Mid(func, i - cont_n, 1) = "-") Then
                    part(j) = part(j) * (-1)
                End If
            End If
            If (part(j) = 0) Then
                part(j) = 1
            End If
            If (exp(j) = 0) Then
                exp(j) = 1
            End If
            If (Mid(func, i, 1) = "-" Or Mid(func, i, 1) = "+") Then
                j += 1
            End If
            num_temp = ""
        Next
        Do While (Abs(x - xini) >= nE Or Abs(Fx) >= nE)
            xini = x
            Fx = 0
            For i = 1 To j Step 1
                If (sem_x(i) = 1) Then
                    xini = 1
                End If
                aux_Fx = (part(i) * (Math.Pow(xini, exp(i))))
                Fx += aux_Fx
                xini = x
            Next
            Fdx = 0
            For i = 1 To j Step 1
                If (sem_x(i) = 1) Then
                    aux_part = part(i)
                    xini = 1
                    part(i) = 0
                End If
                aux_Fdx = ((exp(i) * part(i)) * Math.Pow(xini, exp(i) - 1))
                Fdx += aux_Fdx
                xini = x
                If (sem_x(i) = 1) Then
                    part(i) = aux_part
                End If
            Next
            x = xini - (Fx / Fdx)
            itera += 1
            listIteracao.Items.Add("iteração " & itera)
            listIteracao.Items.Add("xIni " & FormatNumber(xini, 5))
            listIteracao.Items.Add("xNovo " & FormatNumber(x, 5))
            listIteracao.Items.Add("")
        Loop
        listIteracao.Items.Add("xNovo - xIni = " & FormatNumber(x - xini, 5))
        listIteracao.Items.Add("Resultado: " & FormatNumber(x, 5))
    End Sub
    Function Abs(x As Decimal) As Decimal
        x = Math.Sqrt(x * x)
        Abs = x
    End Function


End Class

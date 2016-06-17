#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

float Abs( float x );



int main()
{
    char controle = 's';

    while(controle != 'n')
    {

        float xini = 0., x = 0., Fx = 0., aux_Fx = 0., aux_Fdx = 0, Fdx = 0., E = 0.;
        float  exp[20] = {};
        int    itera= 0, tam = 0, i = 0, j = 0, cont_n = 1, aux_part = 0 ;
        int    part[20] = {};
        int    sem_x[20] = {};
        char   func[30] = {};
        char   numeros[2][20][20] = {};
        char   num_temp[20] = {};
        char   apaga[20] = {"           "};

        system("cls");
        printf("Entre com a FUNCAO: ");
        scanf("%s", func);
        printf("Digite o chute: " );
        scanf("%f", &x );
        printf("Digite o erro: " );
        scanf("%f", &E );

        tam = strlen(func);

        for(i = 0; i < tam; i++)
        {
            if(func[i] >= 48 && func[i] <= 57)
            {
                num_temp[0] = func[i];
                while(func[i+1] >=48 && func[i+1] <= 57)  //alterado i por t
                {
                    num_temp[cont_n] = func[i+1];
                    i++;
                    cont_n++;
                }

                if(func[i+1] == 'x')
                {
                    strcat(numeros[0][j],num_temp);
                    strcpy(num_temp,apaga);
                    part[j] = atoi(numeros[0][j]);
                }

                if(func[i-cont_n] == '^')
                {
                    strcat(numeros[1][j],num_temp);
                    strcpy(num_temp,apaga);
                    exp[j] = atoi(numeros[1][j]);
                }

                if(func[i+1] != 'x' && func[i-cont_n] != '^')
                {
                    strcpy(numeros[0][j],num_temp);
                    num_temp[1] = ' ';
                    part[j] = atoi(numeros[0][j]);
                    sem_x[j] = 1;
                }

                if(func[i-cont_n] == '-')
                    part[j] = part[j]*(-1);

                cont_n = 1;
            }

            if(part[j] == 0)
                part[j] = 1;
            if(exp[j] == 0)
                exp[j] = 1;
            if(func[i] == '-' || func[i] == '+')
            {
                j++;
            }
        }

        do
        {
            xini = x;
            Fx = 0;
            for(i = 0; i <= j; i++)
            {
                if(sem_x[i] == 1)
                {
                    xini = 1;
                }
                aux_Fx = (part[i]*(pow((double)xini,(double)exp[i])));
                Fx = Fx + aux_Fx ;
                xini = x;
            }

            Fdx = 0;
            for(i = 0; i <= j; i++)
            {
                if(sem_x[i] == 1)
                {
                    aux_part = part[i];
                    xini = 1;
                    part[i] = 0;
                }
                aux_Fdx = ((exp[i]*part[i])*pow((double)xini,(double)exp[i]-1));
                Fdx = Fdx + aux_Fdx;
                xini = x;
                if(sem_x[i] == 1)
                    part[i] = aux_part;
            }

            x = xini - ( Fx / Fdx );
            itera++;
            printf( "\niteracao = %d", itera );
            printf( "\nxini = %f\nxnovo = %f", xini, x );

        } while( Abs( x - xini ) >= E || Abs( Fx ) >= E );

        printf( "\n\nxnovo - xini = %f\n", x - xini);
        printf( "Resultado: %.6f\n\n", x );
        printf("Deseja continuar ? s/n ");
        fflush(stdin);
        scanf("%c", &controle);
        fflush(stdin);

    }
    return 0;
}

float Abs( float x )
{
    x = sqrt(x * x);
    return x;
}

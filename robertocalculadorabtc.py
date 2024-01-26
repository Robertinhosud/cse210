import requests
from datetime import datetime
import tkinter as tk
from tkinter import ttk
from threading import Thread

# Variável global para armazenar a proporção ETH/BTC
proporcao_eth_btc = None

# Variáveis globais para armazenar os valores do Bitcoin e Ethereum
valor_btc_usd = None
valor_eth_usd = None
valor_btc_brl = None
valor_eth_brl = None


def obter_valor_bitcoin():
    global valor_btc_usd, valor_eth_usd, valor_btc_brl, valor_eth_brl, proporcao_eth_btc  # Tornando as variáveis globais acessíveis nesta função

    url = "https://api.coingecko.com/api/v3/simple/price"
    parametros = {
        'ids': 'bitcoin,ethereum',
        'vs_currencies': 'brl,usd'
    }

    try:
        resposta = requests.get(url, params=parametros)
        resposta.raise_for_status()
        dados = resposta.json()

        # Obtém o valor do Bitcoin e Ethereum em BRL e USD
        valor_btc_brl = dados['bitcoin']['brl']
        valor_btc_usd = dados['bitcoin']['usd']
        valor_eth_brl = dados['ethereum']['brl']
        valor_eth_usd = dados['ethereum']['usd']

        
        # Retorne a proporção para que possa ser usada fora deste bloco try
        return valor_btc_usd, valor_eth_usd, valor_btc_brl, valor_eth_brl, proporcao_eth_btc

    except requests.exceptions.RequestException as e:
        print(f"Erro ao obter dados da API / Error getting API data: {e}")
        return None

def aguarde_janela():
    janela_aguarde = tk.Tk()
    janela_aguarde.title("Aguarde / Wait")
    tk.Label(janela_aguarde, text="Aguarde enquanto os valores são recuperados... / Please wait while values are retrieved...").pack(padx=20, pady=20)
    return janela_aguarde

def fechar_janela(janela):
    janela.destroy()

def obter_valores_e_fechar_janela(janela):
    obter_valor_bitcoin()
    fechar_janela(janela)

def main_pt():
    agora = datetime.now()
    hora = agora.hour

    if 6 <= hora < 12:
        print("Bom dia!")
    elif 12 <= hora < 18:
        print("Boa tarde!")
    else:
        print("Boa noite!")
    print()
    print('Esta é a calculadora BTC / ETH ')

    # Obtém a data e horário atual
    data_horario_atual = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"Data e Horário (YYYY-MM-DD): {data_horario_atual}")
    print()

    janela_aguarde = aguarde_janela()

    # Inicia uma thread para realizar as operações que podem demorar
    thread_obter_valores = Thread(target=obter_valores_e_fechar_janela, args=(janela_aguarde,))
    thread_obter_valores.start()

    # Adiciona mainloop para a janela principal
    janela_aguarde.mainloop()

    # Exibe os valores
    print('Valor do BTC e ETH em BRL e USD neste momento.')
    print()
    print(f"Bitcoin em BRL: {valor_btc_brl} BRL")
    print(f"Bitcoin em USD: {valor_btc_usd} USD")
    print(f"Ethereum em BRL: {valor_eth_brl} BRL")
    print(f"Ethereum em USD: {valor_eth_usd} USD")

    proporcao_eth_btc = valor_eth_usd / valor_btc_usd
    print(f"Proporção entre ETH e BTC em dolar: {proporcao_eth_btc}")


    print()
    print('No dia 10 de Novembro de 2021 o BTC alcançou o ATH de 69 mil dolares e o ETH de 4 mil e 800 dolares na Binance.')
    print('O valor do ETH era 0.07 do valor do BTC.')
    print()

    # Calcula a proporção novamente para usar na exibição
    proporcao_eth_btc = valor_eth_usd / valor_btc_usd
    print(f"Proporção entre ETH e BTC em dolar hoje: {proporcao_eth_btc:.4f}")

    proporcao_eth_btc = valor_eth_brl / valor_btc_brl
    print(f"Proporção entre ETH e BTC em real hoje: {proporcao_eth_btc:.4f}")
    print()

    print('A melhor proporção foi em Junho de 2017 quando o ETH chegou a 0.156 na Binance.')
    print()
    print('Temos dados históricos que mostram que o preço do ETH tem ficado desde junho 2021') 
    print('na proporçao de 0.05 até 0.08 do preço do BTC ')
    print()
    
    invest = float(input('Qual o valor em dolares voce gostaria de investir:'))
    recev_valor_btc_usd = invest / valor_btc_usd
    recev_valor_eth_usd = invest / valor_eth_usd

    print(f'Com este valor temos {recev_valor_btc_usd:.2f} em BTC ou {recev_valor_eth_usd:.2f} em ETH.')
    print()

    # Aguarda a conclusão da thread antes de encerrar o programa
    thread_obter_valores.join()

    while True:
        # Solicitar ao usuário se deseja inserir um novo valor
        resposta = input('Deseja inserir um novo valor de investimento? (s/n): ').lower()
        if resposta == 's':
            print()
            invest = float(input('Qual o valor em dolares voce gostaria de investir:'))
            recev_valor_btc_usd = invest / valor_btc_usd
            recev_valor_eth_usd = invest / valor_eth_usd

            print(f'Com este valor temos {recev_valor_btc_usd:.2f} em BTC ou {recev_valor_eth_usd:.2f} em ETH.')
            print()
        else:
            print()
            print('Muito Obrigado por usar nossa calculadora, tenha um bom investimento.')
            print()
            # Se a resposta não for 's', saia do loop
            break

            

def main_en():
    agora = datetime.now()
    hora = agora.hour

    if 6 <= hora < 12:
        print("Good Morning!")
    elif 12 <= hora < 18:
        print("Good Afternoon!")
    else:
        print("Good Evening!")
    print()
    print('This is the BTC / ETH calculator ')

    # Get current date and time
    data_horario_atual = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"Date and Time (YYYY-MM-DD): {data_horario_atual}")
    print()

    janela_aguarde = aguarde_janela()

    # Start a thread to perform time-consuming operations
    thread_obter_valores = Thread(target=obter_valores_e_fechar_janela, args=(janela_aguarde,))
    thread_obter_valores.start()

    # Add mainloop for the main window
    janela_aguarde.mainloop()

    # Exibe os valores
    print('Value of BTC and ETH in BRL and USD at this moment.')
    print()
    print(f"Bitcoin in BRL: {valor_btc_brl} BRL")
    print(f"Bitcoin in USD: {valor_btc_usd} USD")
    print(f"Ethereum in BRL: {valor_eth_brl} BRL")
    print(f"Ethereum in USD: {valor_eth_usd} USD")

    proporcao_eth_btc = valor_eth_usd / valor_btc_usd
    print(f"Proportion between ETH and BTC in dollars: {proporcao_eth_btc}")

    print()
    print('On November 10, 2021, BTC reached an ATH of $69,000 and ETH reached $4,800 on Binance.')
    print('The value of ETH was 0.07 of the value of BTC.')
    print()

    # Calculate the ratio again for display
    proporcao_eth_btc = valor_eth_usd / valor_btc_usd
    print(f"Ratio between ETH and BTC in dollars today: {proporcao_eth_btc:.4f}")

    proporcao_eth_btc = valor_eth_brl / valor_btc_brl
    print(f"Ratio between ETH and BTC in real today: {proporcao_eth_btc:.4f}")
    print()

    print('The best ratio was in June 2017 when ETH reached 0.156 on Binance.')
    print()
    print('We have historical data showing that the price of ETH has been in the range of 0.05 to 0.08 of the BTC price since June 2021')
    print()
    invest = float(input('How much in dollars would you like to invest:'))
    recev_valor_btc_usd = invest / valor_btc_usd
    recev_valor_eth_usd = invest / valor_eth_usd

    print(f'With this amount, you have {recev_valor_btc_usd:.2f} in BTC or {recev_valor_eth_usd:.2f} in ETH.')
    print()

    # Wait for the thread to finish before exiting the program
    thread_obter_valores.join()

    while True:
        # Solicitar ao usuário se deseja inserir um novo valor
        resposta = input('Would you like to enter a new investment amount? (y/n): ').lower()
        if resposta == 'y':
            print()
            invest = float(input('How much in dollars would you like to invest:'))
            recev_valor_btc_usd = invest / valor_btc_usd
            recev_valor_eth_usd = invest / valor_eth_usd

            print(f'With this amount, you have {recev_valor_btc_usd:.2f} in BTC or {recev_valor_eth_usd:.2f} in ETH.')
            print()
        else:
            print()
            print('Thank you very much for using our calculator, have a good investment.')
            print()
            # Se a resposta não for 's', saia do loop
            break

# Solicitar ao usuário para escolher o idioma
idioma_escolhido = input('Escolha o idioma (tipo pt para Portugues ou en para Ingles) - Choose the language (type pt for Portuguese or en for English):').lower()
print()
# Executar o programa correspondente ao idioma escolhido
if idioma_escolhido == 'pt':
    main_pt()
elif idioma_escolhido == 'en':
    main_en()
else:
    print('Idioma não reconhecido. O programa será executado em português por padrão.')
    main_pt()

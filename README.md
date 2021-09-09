# Wrench

Este projeto tem como objetivo o desenvolvimento de um software, colocando em prática os conhecimentos obtidos durante a disciplina de Engenharia de Software.
- **Tema**: Plataforma de oferta/demanda de serviços 
- **Objetivo**: Conectar profissionais a pessoas que necessitem serviços

### Descrição 

Todo mundo já passou pela experiência de precisar contratar um serviço e não saber por onde começar. No caso, pode ser desde um passeador de cachorro, professor particular, designer, eletricista, cabeleireiro e por aí vai. Afinal, nem sempre é fácil achar um profissional e, mesmo se isso ocorrer, ainda há a questão de saber se o trabalho é bom ou não. 

Ao mesmo tempo, do outro lado da moeda, profissionais autônomos e freelancers também encontram dificuldades ao buscar clientes e serviços. As principais dificuldades enfrentadas na busca e contratação de um prestador de serviço são: confiança, preço, falta de acesso, disponibilidade, recomendações ou outros motivos. Ao mesmo tempo, há também as dificuldades que alguns prestadores de serviços podem enfrentar, como a falta de visibilidade, poucos clientes, não ter uma forte recomendação no mercado, dentre outros. É aqui que entra o Wrench, criado com o objetivo de unir ambas necessidades. O sistema atua tanto encontrando profissionais para o serviço que você deseja, quanto facilitando que você seja encontrado por clientes potenciais. Essa ideia surgiu da necessidade de facilitar essa busca e conectar quem precisa do serviço com quem presta o serviço. Apesar de existirem no mercado alguns serviços parecidos, percebe-se que eles não atendem a demanda da forma que idealizamos. 

Inicialmente sua construção será dividida em dois apps: um para o demandante e outro para o prestador.  No app do demandante serão desenvolvidas as necessidades básicas para o funcionamento do sistema, tais como:
realizar cadastro, efetuar login, criação de demandas, dentre outras; e no app do prestador serão desenvolvidas funcionalidades adicionais, tais como: permitir que demandante e prestador de serviço estabeleçam uma conversa, avaliação do serviço, avaliação do prestador, entre outras.

A proposta do Wrench é tornar mais fácil a contratação de serviços e o sistema propõe exatamente isso. Para entender um pouco mais sobre os dois lados do sistema proposto, tanto do cliente quanto do profissional, confira como o sistema foi idealizado:

- **Para quem quer contratar**:
Do demandante, o sistema deve permitir que ele se cadastre informando nome completo, escreva um breve resumo do problema que busca solucionar, selecione a categoria de profissional/serviço, veja a lista de prestadores disponíveis, converse com o prestador e então escolha o profissional para efetivação do serviço. Depois de escolhido o prestador, acertado os detalhes de data para realização do serviço e forma de pagamento, o prestador deve realizar o serviço contratado. Após a finalização do mesmo, deve ser efetuado o pagamento (da forma combinada entre as partes) e o demandante realiza avaliação do serviço prestado. Assim que esse preenchimento é concluído, a demanda fica disponível para ser aceita pelo prestador. Uma vez confirmada, o aplicativo lista todas as demandas criadas. Quando uma demanda é aceita pelo prestador, ele sinaliza a data para realização daquele serviço bem como um valor (que pode ser alterado após a finalização do mesmo). Ao final da prestação, o cliente realiza uma avaliação do serviço.  

- **Para quem presta serviços**:
O profissional que tem interesse em começar a atender clientes usando o Wrench terá que se cadastrar na plataforma inserindo nome completo, cnpj ou cpf, os tipos de serviço que presta, bem como a(s) categoria(s) de prestação de serviço. Uma vez cadastrado, o sistema deve permitir que o prestador converse com demandantes, aceite ou rejeite um serviço e sinalize na plataforma que o serviço contratado foi finalizado. Por fim, o sistema irá listar as demandas disponíveis para que o prestadoe entre emcontato com o cliente e combine os detalhes para realização do serviço.

O diferencial do sistema é o poder de escolha tanto para os profissionais quanto para o cliente. O sistema não realizará nenhuma transação financeira. Essa parte deverá ser acordada entre o prestador e demandante.
## Ferramentas utilizadas

- **Front**:
  - ionic 5 + Angular
- **Back**:
  - .NET Core 5.0
## Como executar o projeto

Antes de iniciar o projeto, instale as Ferramentas listadas acima em sua máquina. Além disso, recomendamos o uso do [VSCode](https://code.visualstudio.com/).

```bash
# Clonando o repositório
$ git clone https://github.com/RafaelDAnjos/Wrench

# Acessando o repositório no terminal. Example:
##Executando o back-end:
$ cd /api
$ cd /Wrench.API

## Run
$ dotnet run

## Accesse http://localhost:5100/swagger no browser
## Executando o front-end Demandante:
$ cd /app-client
$ cd /Wrench-app

## Run
$ ionic serve

##Acesse http://localhost:8100 no browser
## Executando o front-end Prestador:
$ cd /app-prestador
$ cd /Wrench-prestador

## Run
$ ionic serve --port:8080
##Acesse http://localhost:8080 no browser


```
<hr>
## Colaboradores

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/RafaelDAnjos">
        <img src="https://github.com/RafaelDAnjos.png" width="100px;" alt="Foto de Rafael dos Anjos"/><br>
        <sub>
          <b>Rafael dos Anjos</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/LucasErlacher">
        <img src="https://github.com/LucasErlacher.png" width="100px;" alt="Foto de Lucas Erlacher"/><br>
        <sub>
          <b>Lucas Erlacher</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/mellyssaStephanny">
        <img src="https://github.com/mellyssaStephanny.png" width="100px;" alt="Foto de Mellyssa Stephanny"/><br>
        <sub>
          <b>Mellyssa Stephanny</b>
        </sub>
      </a>
    </td>
  </tr>
</table>



# Wrench

### Descrição do minimundo

O projeto Wrench tem como objetivo a construção de um sistema que conecta usuários com prestadores de serviços. Essa ideia surgiu da necessidade de facilitar essa busca e conectar quem precisa do serviço com quem presta o serviço. Apesar de existirem no mercado alguns serviços parecidos, percebe-se que eles não atendem a demanda da forma que idealizamos. As principais dificuldades enfrentadas na busca e contratação de um prestador de serviço (residencial ou não) são: confiança, preço, falta de acesso, disponibilidade, recomendações ou outros motivos. Ao mesmo tempo, há também as dificuldades que alguns prestadores de serviços podem enfrentar, como a falta de visibilidade, poucos clientes, não ter uma forte recomendação no mercado, dentre outros. 

Inicialmente sua construção será dividida em dois módulos. No primeiro módulo serão desenvolvidas as necessidades básicas para o funcionamento do sistema, tais como:
realizar cadastro, efetuar login, criação de demandas, dentre outras; e no segundo serão desenvolvidas funcionalidades adicionais, tais como: permitir que demandante e prestador de serviço estabeleçam uma conversa, avaliação do serviço, avaliação do prestador, entre outras.

Do demandante (quem solicita o serviço), o sistema deve permitir que ele se cadastre informando nome completo, escreva um breve resumo do problema que busca solucionar, selecione a categoria de profissional/serviço, veja a lista de prestadores disponíveis, converse com o prestador e então escolha o profissional para efetivação do serviço. Depois de escolhido o prestador, acertado os detalhes de data para realização do serviço e forma de pagamento, o prestador deve realizar o serviço contratado. Após a finalização do mesmo, deve ser efetuado o pagamento (da forma combinada entre as partes) e o demandante realiza avaliação do serviço prestado.

No perfil do prestador (pessoa física ou jurídica que presta um serviço), o sistema deve permitir que ele se cadastre inserindo nome completo, cnpj ou cpf, os tipos de serviço que presta e também inserir a(s) categoria(s) de prestação de serviço, converse com demandantes, aceite ou rejeite um serviço e sinalize na plataforma que o serviço contratado foi finalizado.


## Requisitos Funcionais

| RF001 |  Cadastro de prestador de serviço  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir o cadastro do prestador de serviços na plataforma. |
|  Prioridade |  Alta | 

| RF002 |  Cadastro de demandante  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir o cadastro do demandante na plataforma. |
|  Prioridade |  Alta | 

| RF003 |  Criar Demanda  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que demandantes consigam criar uma demanda. |
|  Prioridade |  Alta | 

| RF004 |  Listar/Visualizar Demandas (Prestador de Serviço)  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve listar para o prestador de serviços todas as demandas que contiverem uma tag que é de seu interesse.|
|  Prioridade |  Alta | 

| RF005 |  Listar/Visualizar Demandas (Demandante)  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve listar as demandas ativas para o demandante.|
|  Prioridade |  Alta |

| RF006 |  Comunicação entre prestador de serviço e demandante  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que os prestadores de serviços possam se comunicar com os possíveis clientes.|
|  Prioridade |  Alta |

| RF008 |  Topar demanda  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que o prestador de serviços e o demandante topem realizar a demanda.|
|  Prioridade |  Alta |

| RF009 |  Recusar Demanda  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que o prestador de serviços ou o demandante recusem a realização da demanda.|
|  Prioridade |  Alta |

| RF010 |  Exibir contatos  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve exibir o contato do prestador de serviços ao demandante. O sistema deve exibir o contato do demandante ao prestador.|
|  Prioridade |  Alta |

| RF011 |  Estabelecer prazo para prestação de serviço  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve pedir que o prestador de serviços informe qual a data limite para a conclusão daquela demanda.|
|  Prioridade |  Alta |

| RF012 |  Definir valor previsto  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve pedir que o prestador de serviços informe qual o valor previsto para a conclusão daquele serviço.|
|  Prioridade |  Alta |

| RF013 |  Concluir Serviço  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que o demandante e o prestador de serviços concluam a demanda, informando data de conclusão e valor praticado.|
|  Prioridade |  Alta |

| RF014 |  Negar Serviço  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que o demandante e o prestador de serviços cancelem a execução do serviço. |
|  Prioridade |  Alta |

| RF015 |  Avaliar prestador  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que um demandante avalie um prestador caso o serviço tenha sido realizado. |
|  Prioridade |  Alta |

| RF016 |  Avaliar cliente  |
| ------------------- | ------------------- |
|  Descrição | O sistema deve permitir que um prestador, caso o serviço tenha sido realizado, avalie o seu cliente. |
|  Prioridade |  Alta |




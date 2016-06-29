# #AzureSolutionlet - Aplicação sem servidores + Serviços Cognitivos

##Olympics-Meter
Análise de sentimento sobre seu país durante as olimpíadas do Rio de 2016.

> Azure Solutionlets são soluções rápidas para problemas que podem ser resolvidos com pequenos deploys, utilizando o Microsoft Azure. Neste app, você aprenderá a criar e publicar uma aplicação distribuída que não necessita de servidores e utiliza os serviços cognitivos da Azure. 

### Passo 0: Criar conta trial do Microsoft Azure
Acesse https://azure.microsoft.com/pt-br/pricing/free-trial/ e clique no botão **Teste agora**:

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img01.png)

Logue-se com uma conta Microsoft (hotmail, live, etc). Em seguida, preencha seus dados. É necessário um telefone celular para verificar sua identidade, bem como um cartão de crédito válido. Após ler os termos e, caso concorde com eles, cheque *Eu concordo..."* seguido do clique em **Inscrever-se**:

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img02.png)

Você será uma levado a uma página onde deve aguardar alguns instantes até que sua subscrição esteja pronto para uso. Uma vez pronta, clique no botão verde para continuar e ser levado à tela inicial do Microsoft Azure:

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img03.png)

### Passo 1: Executando o projeto
Clone este projeto em sua máquina e abra o Index.html com seu browser preferido. A página deve abrir, porém os dados que dependem dos serviços não serão carregados por que devemos configurar os endpoints das APIs.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img04.png)

### Passo 2: Criando uma conta de Storage no Azure
Para criar uma nova conta de Storage, no menu lateral de seu dashboard no Azure, clique em **New** > **Data + Storage** > **Storage Account**. Insira informações como nome, assinatura e resource group. Você poderá criar um resource group durante a configuração da sua conta de storage ou selecionar um [resource group](https://azure.microsoft.com/pt-br/documentation/articles/resource-group-portal/) já existente.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img05.png)

### Passo 3: Tabelas para armazenamento dos dados 
Depois de criar uma nova conta de Storage, vamos criar uma tabela para armazenar os dados dos países e uma tabela para armazenar a chave de acesso ao Azure cognitive services. Abaixo está o comando para criar as tabelas utilizando Azure Cli, mas você pode escolher outras abordagens de administração.

```
export AZURE_STORAGE_ACCESS_KEY=<CHAVE DE ACESSO>
export AZURE_STORAGE_ACCOUNT=<NOME DA CONTA DE STORAGE>
```
Com a conta de Storage devidamente configurada, agora temos acesso para criar as tabelas
```
azure storage table create Country
azure storage table create Keys
```
Para garantir que as tabelas foram criadas com sucesso, liste todas e verifique
```
azure storage table list
```

### Passo 4: Back-end : Azure functions
Para criar um novo Function App, no menu lateral de seu dashboard no Azure, clique em **New** > **Web + Mobile** > **Function App**. Insira informações como nome, assinatura, resource group e storage account.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img06.png)

### Passo 5: Publicando as funcões de back-end
Para publicar as funções você pode copiar e colar as funções do projeto clonado na sua maquina ou configurar a integração contínua a partir de algum serviço de repositório. Abaixo mostra a configuração a partir do GitHub. Clique em Sync para garantir.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img07.png)
![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img08.png)

Com as funções criadas, edite a aba Integrate para configurar a sua conta de Storage, para que ela fique acessível a partir da função.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img09.png)

Copie os endpoints das APIs geradas para configurarmos o front-end

### Passo 6: Front-end : Arquivos estáticos (HTML, JS e CSS) 
Agora que temos os endpoints das APIs geradas, vamos editar o arquivo **Site** > **JS** > **olympics.js**.

Depois de criar uma nova conta de Storage, vamos publicar os arquivos do front-end em um container Blob, que ficará acessível ao público. Para isso clique em **Blob** > **+ Container**. Insira o nome do seu aplicativo para gerar sua URL pública de acesso ao web app, e no Access type, selecione Blob.
```
var APIContry = "<ENDPOINT DA API COUNTRY>"
var APIMessage = "<ENDPOINT DA API MESSAGE>"
```
Agora vamos publicar os arquivos estáticos no nosso container Blob. Isso pode ser feito de diversas maneiras como API Rest, Azure Cli, entre outros. , Para usuários de MacOS, uma das maneiras é utilzar o cliente Microsoft Azure Storage Explorer (http://storageexplorer.com/). Nele você pode configurar as chaves da sua conta de storage e publicar seus arquivos com uma interface parecida a de um FTP.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img10.png)

### Passo 7: Serviços cognitivos : Text Analytics
O último serviço que falta criar na aplicação é o Azure cognitive services. Para criar um novo Cognitive services, no menu lateral de seu dashboard no Azure, clique em **New** > **Data + Analytics** > **Cognitive Services**. Insira informações como nome, assinatura e resource group. Em API Type, selecione Text Analytics.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img11.png)

Por fim, vamos cadastrar a chave de acesso na tabela de chaves. Isso pode ser feito de diversas maneiras como API Rest, Azure Cli, entre outros. , Para usuários de MacOS, uma das maneiras é utilzar o cliente Microsoft Azure Storage Explorer (http://storageexplorer.com/). Nele você editar as tabelas na sua conta de storage.

![](https://raw.githubusercontent.com/panazzo/olympics-meter/master/images/img12.png)


#### Parabéns! Sua aplicação está funcionando, e sem servidores!

HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://www.situacaocadastral.com.br/");
//Site para a consulta
//Site for the consultation

webRequest.Method = "POST";
//Fazer um post
//Make a post

byte[] byteArray = Encoding.UTF8.GetBytes("doc=" + cpf + "&" + "751d31dd6b56b26b29dac2c0e1839e34=" + "NTFkODFkNGZjZGJlYzc5ZWMzZDE2OWVlODk4NDU4NmR8TW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzYzLjAuMzIzOS4xMzIgU2FmYXJpLzUzNy4zNnxodHRwczovL3d3dy5zaXR1YWNhb2NhZGFzdHJhbC5jb20uYnIvfGh0dHBzOi8vd3d3LnNpdHVhY2FvY2FkYXN0cmFsLmNvbS5ici98dHJ1ZXwxMzY2eDc2OHgyNHwxMzY2eDE4MXwwfDB8MTIzMA%3D%3D");
//Passa para bytes o cpf e informações para ele aceitar como validação o pedido (Ele pede lugar onde clicou, user-agent etc e manda junto em base64 necessário para validar, inseguro.
//It passes to bytes the cpf and information for it to accept as validation the request (It asks place where clicked, user-agent etc and sends together in base64 necessary to validate, insecure.


webRequest.ContentType = "application/x-www-form-urlencoded";
//Tipo de request
//Type of request

webRequest.ContentLength = byteArray.Length;
//Passa o tamanho do header
//Send length of header

Uri os = new Uri("https://www.situacaocadastral.com.br/");
//Url denovo
//Url again

CookieContainer coc = new CookieContainer();
//Cria um, cookie vazio
//Create a cookie empty

string temp = cpf;
//Armazena uma cópia do cpf
//Stores a copy of cpf
	
while(temp.IndexOf(".") != -1){
	temp = temp.Replace(".", "");
}
//Remove todo os pontos
//Remove all dots

temp = temp.Replace("-", "");
//Remove o único traço / hífen
//Remove the "-"

coc.SetCookies(os,"_jsuid=4241404445,__cfduid=db2a621a244a7a4d5498df820266e657a1517618238,USID=11c2568900823bffd85e966db7f8fc69,__utmc=180780453,__utmz=180780453.1517618240.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none),__utma=180780453.1885155002.1517618240.1517618240.1517618240.1,heatmaps_g2g_100536568=yes,__utmt=1,_first_pageview=1,__utmb=180780453.8.10.1517618240,_eventqueue=%7B%22heatmap%22%3A%5B%7B%22type%22%3A%22heatmap%22%2C%22href%22%3A%22%2Fcpf-" + temp + ".html%22%2C%22x%22%3A1129%2C%22y%22%3A99%2C%22w%22%3A1366%7D%5D%2C%22events%22%3A%5B%5D%7D");
//Passa o cpf apenas números com o cookie e as info do usuário para simular um user normal.*Os cookies são para a url do situação cadastral*
//Pass the cpf only with the cookie and as user information to simulate a normal user. * Cookies are for a url of the cadastral situation *

webRequest.CookieContainer = coc;
//Passa o cookie para a request
//Send the cookie to request

webRequest.Referer = "https://www.situacaocadastral.com.br/";
//A referência que vai estar no header do post é do próprio site
//The reference that will be in the header of the post is the site itself

using (Stream webpageStream = webRequest.GetRequestStream()){
webpageStream.Write(byteArray, 0, byteArray.Length);
}
//Envia os dados
//Send data

WebResponse webResponse = webRequest.GetResponse();
//Pega a resposta do servidor
//Get the response of server

Stream st = webResponse.GetResponseStream();
//Pega a resposta em forma de Stream
//Get the response in form of Stream

StreamReader sr = new StreamReader(st);
//Passa para o stream Reader
//Semd to stream Reader

string test = sr.ReadToEnd();
//Passa todo o conteúdo em html para a string test
//Send all to string(the format is the html returned of request) 

st.Close();
sr.Close();


if(test.IndexOf("<span class=\"dados\"") > -1) {
	test = test.Remove(0,test.IndexOf("<span class=\"dados\">") + 20);
	string Nome = test.Substring(0,test.IndexOf("</span>"));
  //Essa pega o nome da pessoa, mude de lugar essa string!
  //Get the name, set new location for this string
}
if(test.IndexOf("Situação: Regular") == -1){ //Se é regular    / if is regular
	return false;
}else{
	return true;
}



/*

By:RC0D3

Cpf pode ser um cpnj!!!
Eu creio

The cpf can be in cnpj format!!
I believe


EXTRAS


Possíveis dados do *cpf* exatamente assim:
Possible * cpf * data exactly like this:

Regular
inválido
Inexistente
Falecido


Tradução / Translation : Google translate / me

Obrigado / Thanks
*/

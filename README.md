# SearchScraper
SearchScraper is a SERP scraper that doesn't use any third party libraries for it's functionality.
## The High Level
The application presents the user with two fields, one for keywords and one for a url.
When submitted the "callurl" function is called.

callurl first takes the "keywords" and chains is through 3 functions.

## returnWebResponseString
Makes an HttpWebRequest with the keywords tacked onto the end of the bulk of the google search query. (https://www.google.com/search?num=100&q=)
The response is converted into a string response with a StreamReader class.
Note: In the future I will make the "num-100" into a variable, to open up the options for the user to select a larger or smaller number of results.

## returnMatchCollection
When manually parsing through the HttpWebRequest I found that the class that held the URLs for the search results began with "BNeawe UPmit AP7Wnd" if it had no extension and "BNeawe UPmit AP7Wnd lRVwie" if it does.  
So, to get a collection of URLs I used regular expressions to return the matches. Regex("\\bBNeawe UPmit AP7Wnd( lRVwie\">|\">)([^\\s<]+)"). 

## returnMatchCollectionDict
My final transformation took the Matches and converted it to a Dictionary with the Key being the URLs and the Values being a List<int> of the Indexes.  For each match(url) I increased the counter and added it to the dictionary entries value or added a whole new entry if my loop hadn't found that key before.  The reason I went this route is in case in the future, the client would want a larger breakdown of the results, like a ranking of the most popular URLs for example.


## Back to callurl
With the dictionary in hand a quick "if(urlDictionary.ContainsKey(url))" allows me to check if the url passed by the user appears in the search results and return the values(the indexes the results appeared in), as a joined string. 



## Things to note/what comes next.
This was my first time using knockout.js and a lot more time than anticipated was spent learning how to use it and I know there's more I could have done with it.  My last sticking point was taking the results from the "callWebScraper" function and revealing a hidden h3 element.  When that is settled I'd move from using the alert to simply having the results render on screen.
While I'm happy with the Single Responsibility of my main functions in "Controllelrs/WebScraper.cs" I would switch to true generics then move onto unit tests.
HttpWebRequest is depreciated and I will need to learn more about its new replacement.

  

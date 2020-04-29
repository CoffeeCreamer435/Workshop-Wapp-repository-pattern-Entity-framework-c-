Een ORM framework zorgt ervoor dat een Query gebouwd wordt voor de juiste relationele database. Dit wordt gedaan met behulp van het ORM (Object relational mapping). Dit heeft als voordeel dat de ontwikkelaar maar één ORM query hoeft te schrijven voor meerdere relationele databases.


![Meme](https://i.stack.imgur.com/dE5Q6.png)

In figuur 1 is te zien hoe EF, de ORM query verwerkt voor de ontwikkelaar. De eerste laag is de applicatie laag. Deze laag bouwt de programmeur met behulp van models en logica. 
De volgende laat is de Entity framework. EF communiceert met behulp van het ORM-interface.
EF communiceert op zijn beurt ook weer met de ADO.NET provider. Deze provider zet de EF-query’s om in een relationele database query.
De laatste laag in dit figuur 1 is de Data Store. Deze laag is de database.


Code compiles with Mono and can be run on OSX.

To run code on OSX
-------------------
In root folder run following command
$ chmod +x poker.sh

Example usages:

1.
$ ./poker.sh -p[AD KD QD JD 10D] -p[4H 4C 3S 3D AS] -poker

Will output:
```
--------------------------------------------
[AD KD QD JD 10D] Royal Flush
[4H 4C 3S 3D AS] Two Pairs 4s & 3s
--------------------------------------------
Winning Hand:  [AD KD QD JD 10D] Royal Flush
Press any key...
```

2.
$ ./poker.sh -texas:[AS KD 2H 5S 5C] -p:[3D 5H] -p:[KS JD] -p:[5D KH]

Will output:
```
--------------------------------------------
[5H 5S 5C KD 3D] Three Of A Kind
[KS KD 5S 5C JD] Two Pairs Ks & 5s
[5D 5S 5C KH KD] Full House
--------------------------------------------
Winning Hand:  [5D 5S 5C KH KD] Full House
Press any key...
```
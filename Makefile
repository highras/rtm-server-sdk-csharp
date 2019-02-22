test.exe:
	mcs /reference:System.Net.Http.dll,System.Json.dll RTMServerClient.cs test.cs -out:test.exe

clean:
	rm -f test.exe

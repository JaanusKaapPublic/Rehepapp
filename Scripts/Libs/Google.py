import requests

def doGoogleSearch(input, start=0, count=100, countryDomain = "ee"):
	listOfFiles = []

	if count == 0:
		r = requests.get('http://www.google.' + countryDomain + '/search?hl=en&q=' + input + '&num=' + str(
			count) + '&btnG=Google+Search')
	else:
		r = requests.get('http://www.google.' + countryDomain + '/search?hl=en&q=' + input + '&num=' + str(
			count) + '&start=' + str(start))

	pos = r.content.find('<a href="')
	while pos != -1:
		pos2_a = r.content.find('"', pos + 16)
		pos2_b = r.content.find('&amp;', pos + 16)
		if pos2_a == -1:
			pos2 = pos2_b
		elif pos2_b == -1:
			pos2 = pos2_a
		else:
			pos2 = min(pos2_a, pos2_b)

		if pos2 == -1:
			break

		url = r.content[pos + 16:pos2]
		if url.find('.google.') == -1 and url.startswith('http'):
			listOfFiles.append(url)

		pos_a = r.content.find('<a href="', pos + 1)
		pos_b = r.content.find('a href="/url?q=', pos + 1)
		if pos_a == -1:
			pos = pos_b
		elif pos_b == -1:
			pos = pos_a
		else:
			pos = min(pos_a, pos_b)

	return listOfFiles
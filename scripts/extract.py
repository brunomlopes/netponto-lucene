from xml.etree.ElementTree import iterparse
directory = r"F:\ptwiki-latest-pages-articles.xml"

context = iterparse(directory+"\ptwiki-latest-pages-articles.xml", events=("start", "end"))

# turn it into an iterator
context = iter(context)

# get the root element
event, root = context.next()

namespace = "{http://www.mediawiki.org/xml/export-0.4/}"
i = 1

for event, elem in context:
    if event == "end" and elem.tag == namespace+"page":

        title = elem.find(namespace+"title").text
        text =  elem.find(namespace+"revision").find(namespace+"text").text
        
        if text == None \
                or len(set(title).intersection(":\\/")) > 0 \
                or text[:20].lower().startswith("#redirect") \
                or text[:20].lower().startswith("{{desambigua"): 
            continue

        try:
            with file(directory+r"\files\%s.txt" % title, "w") as f:
                f.write(text.encode("utf-8"))

        except Exception,e:
            print "Error with %s : %s" % (title, e)
            root.clear()
            continue

        if i >= 1000: break
        i += 1

        root.clear()

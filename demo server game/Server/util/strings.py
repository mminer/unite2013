"""Functions for manipulating strings."""

import HTMLParser

__copyright__ = "Copyright 2012, Rebel Hippo Inc."


HTML_CHAR_MAP = {
    u'\xc1': '&Aacute;',
    u'\xe1': '&aacute;',
    u'\xc0': '&Agrave;',
    u'\xc2': '&Acirc;',
    u'\xe0': '&agrave;',
    u'\xe2': '&acirc;',
    u'\xc4': '&Auml;',
    u'\xe4': '&auml;',
    u'\xc3': '&Atilde;',
    u'\xe3': '&atilde;',
    u'\xc5': '&Aring;',
    u'\xe5': '&aring;',
    u'\xc6': '&Aelig;',
    u'\xe6': '&aelig;',
    u'\xc7': '&Ccedil;',
    u'\xe7': '&ccedil;',
    u'\xd0': '&Eth;',
    u'\xf0': '&eth;',
    u'\xc9': '&Eacute;',
    u'\xe9': '&eacute;',
    u'\xc8': '&Egrave;',
    u'\xe8': '&egrave;',
    u'\xca': '&Ecirc;',
    u'\xea': '&ecirc;',
    u'\xcb': '&Euml;',
    u'\xeb': '&euml;',
    u'\xcd': '&Iacute;',
    u'\xed': '&iacute;',
    u'\xcc': '&Igrave;',
    u'\xec': '&igrave;',
    u'\xce': '&Icirc;',
    u'\xee': '&icirc;',
    u'\xcf': '&Iuml;',
    u'\xef': '&iuml;',
    u'\xd1': '&Ntilde;',
    u'\xf1': '&ntilde;',
    u'\xd3': '&Oacute;',
    u'\xf3': '&oacute;',
    u'\xd2': '&Ograve;',
    u'\xf2': '&ograve;',
    u'\xd4': '&Ocirc;',
    u'\xf4': '&ocirc;',
    u'\xd6': '&Ouml;',
    u'\xf6': '&ouml;',
    u'\xd5': '&Otilde;',
    u'\xf5': '&otilde;',
    u'\xd8': '&Oslash;',
    u'\xf8': '&oslash;',
    u'\xdf': '&szlig;',
    u'\xde': '&Thorn;',
    u'\xfe': '&thorn;',
    u'\xda': '&Uacute;',
    u'\xfa': '&uacute;',
    u'\xd9': '&Ugrave;',
    u'\xf9': '&ugrave;',
    u'\xdb': '&Ucirc;',
    u'\xfb': '&ucirc;',
    u'\xdc': '&Uuml;',
    u'\xfc': '&uuml;',
    u'\xdd': '&Yacute;',
    u'\xfd': '&yacute;',
    u'\xff': '&yuml;',
    u'\xa9': '&copy;',
    u'\xae': '&reg;',
    u'\u2122': '&trade;',
    u'\u20ac': '&euro;',
    u'\xa2': '&cent;',
    u'\xa3': '&pound;',
    u'\u2018': '&lsquo;',
    u'\u2019': '&rsquo;',
    u'\u201c': '&ldquo;',
    u'\u201d': '&rdquo;',
    u'\xab': '&laquo;',
    u'\xbb': '&raquo;',
    u'\u2014': '&mdash;',
    u'\u2013': '&ndash;',
    u'\xb0': '&deg;',
    u'\xb1': '&plusmn;',
    u'\xbc': '&frac14;',
    u'\xbd': '&frac12;',
    u'\xbe': '&frac34;',
    u'\xd7': '&times;',
    u'\xf7': '&divide;',
    u'\u03b1': '&alpha;',
    u'\u03b2': '&beta;',
    u'\u221e': '&infin',
}


def special_chars_to_html(string):
    """Replaces special characters in a block of text to their HTML codes."""
    string = unicode(string, 'utf-8')
    new_string = ''.join(HTML_CHAR_MAP.get(c, c) for c in string)
    return new_string


def html_to_special_chars(string):
    """Replaces HTML-encoded special characters with unicode."""
    parser = HTMLParser.HTMLParser()
    new_string = parser.unescape(string)
    return new_string

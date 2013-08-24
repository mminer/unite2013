import json
import os
from google.appengine.api import urlfetch

__copyright__ = "Copyright 2013, Rebel Hippo Inc."


def is_local_host():
    """Checks if the server is running on localhost or not."""
    return 'localhost' in os.environ['SERVER_NAME']


def get_base_url():
    """Returns the base path for the API."""
    if is_local_host():
        base_url = 'http://localhost:8888/api/'
    else:
        base_url = 'https://www.lumospowered.com/api/'
    
    return base_url


def redirect_to_lumospowered(api, data):
    """Redirects an API call to https://lumospowered.com."""
    url = get_base_url() + api
    json_data = json.dumps(data)
    headers = { 'Content-Type': 'application/json' }
    result = urlfetch.fetch(url=url, payload=json_data, method=urlfetch.POST,
                            headers=headers, deadline=60)
    return result.content

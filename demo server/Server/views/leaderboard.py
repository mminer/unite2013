from hashlib import sha1
import json
import webapp2
from models import LeaderboardEntry
import view

__copyright__ = "Copyright 2012, Rebel Hippo Inc."


class IndexHandler(webapp2.RequestHandler):
    def get(self, leaderboard_id):
        qry = LeaderboardEntry.query()
        qry = qry.filter(LeaderboardEntry.leaderboard_id == leaderboard_id)
        qry = qry.order(-LeaderboardEntry.score)
        scores = qry.fetch(100)
        view.render(self, 'leaderboard.html', scores=scores)
    
    def post(self, leaderboard_id):
        data = json.loads(self.request.body)
        entry = LeaderboardEntry(leaderboard_id=leaderboard_id, 
                                 name=data['name'], score=data['score'])
        entry.put()
        self.response.write('Leaderboard entry saved.')


class HashedScoreHandler(webapp2.RequestHandler):
    def post(self, leaderboard_id):
        data = json.loads(self.request.body)
        secret_key = 'unite2013'
        our_hash = sha1(str(data['score']) + secret_key).hexdigest()

        if our_hash != data['hash']:
            self.response.write('Nice try, cheater!')
        else:
            entry = LeaderboardEntry(leaderboard_id=leaderboard_id, 
                                     name=data['name'],  score=data['score'])
            entry.put()
            self.response.write('Leaderboard entry saved.')

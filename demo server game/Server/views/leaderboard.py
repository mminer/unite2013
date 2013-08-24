import json
import webapp2
from models import LeaderboardEntry
from util import view

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
        entry = LeaderboardEntry(leaderboard_id=leaderboard_id, name=data['name'],
                                 score=data['score'], )
        entry.put()
        self.response.write('Leaderboard entry saved.')

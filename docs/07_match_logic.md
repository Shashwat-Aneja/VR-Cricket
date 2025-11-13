# Match Logic — Technical Documentation

The match logic handles scoring, ball resets, runs, overs, boundaries, and wickets.

---

## 🔢 Scoring Rules Implemented

### 1. Runs
- Automatic detection based on ball travel
- Manual addition from fielding logic

### 2. Boundaries
If ball travels more than `boundaryDistance`:


### 3. Overs
Each over = 6 balls.  
Overs = `ballsBowled / 6`.

### 4. Wickets
Manual or event-driven:

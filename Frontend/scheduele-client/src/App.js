import './App.css';
import {BrowserRouter as Router , Switch , Route} from 'react-router-dom'
import MainHome from './HomeComponents/MainHome';
import Secure from './Utils/Secure';
import LandingPage from './LandingComponents/Landing';
function App() {
  return (

    <Router>
      <div className="App">
          <Switch>
            <Route exact path="/" component={MainHome} />
            <Secure component = {LandingPage} path="/landing"/> 
          </Switch>     
      </div>
    </Router>
  );
}

export default App;

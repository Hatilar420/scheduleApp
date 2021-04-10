import React, { Component } from 'react'
import GoogleLogin from 'react-google-login';
import tokenUtil from '../Utils/TokenUtil'
import extraUtil from '../Utils/ExtraUtils'
import { Redirect } from 'react-router-dom';
import '../Landing.css';

export default class MainHome extends Component {
    constructor(props) {
        super(props);
        this.axios = require('axios').default;
        this.state={
            UserName : "",
            PassWord : ""
        }
    }

    UserNameUpdate = (event) =>{
        this.setState({ UserName : event.target.value  }, () => {
            console.log(this.state.UserName)
        })
    }

    PasswordUpdate = (event) =>{
        this.setState({ PassWord : event.target.value  }, () => {
            console.log(this.state.PassWord)
        })
    }

    OnGoogleSuccess = (data) =>{
        //console.log(data)

        this.axios.post(`https://localhost:${extraUtil.PortNumber}/api/verify`,data)
        .then(res =>{
             
              tokenUtil.token = res.data.token;
              //console.log(tokenUtil.token)
              tokenUtil.IsAuthenticated = true;
              if(this.props.history !== undefined)
              {
              this.props.history.push("/landing");
              }
              else{
                   this.forceUpdate()
              }
        }).catch(err =>{
                console.log(err)
        })

    }

    OnGoogleFailure = (data) =>{
        console.log(data)
    }

    render() {
        if(tokenUtil.IsAuthenticated)
        {
          return (<Redirect to="/landing"/>)
        }
        else
        {

            return (
                <div className="HomeDiv">
                    <div>
                        <h1>Login here</h1>
                    </div>
                    <div>
                    <GoogleLogin
                        clientId={extraUtil.ClientSecret}
                         buttonText="Login"
                         onSuccess={this.OnGoogleSuccess}
                        onFailure={this.OnGoogleFailure}
                        isSignedIn={true}
                        cookiePolicy={'single_host_origin'}
      />
                    </div>
                </div>
            )

        }

        
    }
}

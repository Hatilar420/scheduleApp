import React from 'react'
import{Redirect, Route} from  'react-router-dom'
import tokenUtil from "./TokenUtil"

export default function Secure({component:Component,...rest}) {
    return (
        <div>
         <Route {...rest} render={
             (props) =>{
                if(tokenUtil.IsAuthenticated){
                   return <Component {...props}/>
                }
                else{
                    console.log("Not Authenticated")
                    return <Redirect to="/" />
                }

             }
         }  />
        </div>
    )
}
import React, { Component } from 'react'
import * as dateFns from 'date-fns'
import tokenUtil from '../Utils/TokenUtil'
import { BsXCircleFill, BsCircleFill } from "react-icons/bs";
import extraUtil from '../Utils/ExtraUtils'

export default class LandingPage extends Component {


    constructor(props) {
        super(props);
        this.axios = require('axios').default;
        this.state = {
            TaskName : "",
            TaskLocation : "",
            isFreeTime  : false,
            currentMonth : new Date(),
            selectedDate : new Date(),
            selectedWidgetDate : new Date(),
            ShowSetReminderDiv : false,
            Tasks : {},
            StartTime : "" ,
            EndTime : ""
        }
    }

    componentDidMount(){
    
        this.axios.get(`https://localhost:${extraUtil.PortNumber}/api/task/get`, {headers:{
            "Authorization":"Bearer " + tokenUtil.token
            }}).then( res =>{
                //console.log(res.data)
                for(let i of res.data){
                    this.createTask(i,false)
                }
            } )

    }

    createTask = (obj,FromReact) =>{
        let temptask = this.state.Tasks;
        let tempobj
        if(FromReact){
            tempobj =  {
                taskName : obj.TaskName,
                isFree : obj.IsFree,
                location : obj.Location,
                startTime : obj.StartTime,
                endTime : obj.EndTime
            }
        }
        else{
            tempobj =  {
                taskName : obj.taskName,
                isFree : obj.isFree,
                location : obj.location,
                startTime : obj.startTime,
                endTime : obj.endTime
            }
        }
        //Create if it doesn't exist 
        if(temptask[obj.date] === null || temptask[obj.date] === undefined){
            temptask[obj.date] = [tempobj]
        }
        else{
             temptask[obj.date].push(tempobj)
        }
        this.setState({
            Tasks : temptask
        })

    }

    nextMonth = () => {
        this.setState({
          currentMonth: dateFns.addMonths(this.state.currentMonth, 1)
        });
      };

      prevMonth = () => {
        this.setState({
          currentMonth: dateFns.subMonths(this.state.currentMonth, 1)
        });
      };

      onDateClick = day => {
           let tempDate  =   new Date(day)
            //let CorrectedDate = dateFns.addDays(tempDate,1);
            this.setState({
                selectedWidgetDate :tempDate,
                ShowSetReminderDiv : true
            } , () =>{
                //console.log(this.state.selectedWidgetDate)
            })
            
          
      };


    renderHeader() {
        const dateFormat = "MMMM yyyy";
        return (
          <div className="header row flex-middle">
            <div className="col col-start">
              <div className="icon" onClick={this.prevMonth}>
                chevron_left
              </div>
            </div>
            <div className="col col-center">
              <span>
                {dateFns.format(this.state.currentMonth, dateFormat)}
              </span>
            </div>
            <div className="col col-end" onClick={this.nextMonth}>
              <div className="icon">chevron_right</div>
            </div>
          </div>
        );
      }


      renderDays() {
        const dateFormat = "iiii";
        const days = [];
        let startDate = dateFns.startOfWeek(this.state.currentMonth);
        for (let i = 0; i < 7; i++) {
          days.push(
            <div className="col col-center" key={i}>
              {dateFns.format(dateFns.addDays(startDate, i), dateFormat)}
            </div>
          );
        }
        return <div className="days row">{days}</div>;
      }


      renderCells() {
        const { currentMonth, selectedDate } = this.state;
        const monthStart = dateFns.startOfMonth(currentMonth);
        const monthEnd = dateFns.endOfMonth(monthStart);
        const startDate = dateFns.startOfWeek(monthStart);
        const endDate = dateFns.endOfWeek(monthEnd);
    
        const dateFormat = "d";
        const rows = [];
    
        let days = [];
        let day = startDate;
        let formattedDate = "";
    
        while (day <= endDate) {
          for (let i = 0; i < 7; i++) {
            formattedDate = dateFns.format(day, dateFormat);
            const cloneDay = day;
           let taskOfTheDay =   this.state.Tasks[cloneDay.toISOString()]
           let renderTasks = undefined
           if(taskOfTheDay !== undefined){
            renderTasks = taskOfTheDay.map( (x) => <li className="ListText">  { x.isFree ? <BsCircleFill className="ListCircleGreen"/>  : <BsCircleFill className="ListCircleRed"/> }   {x.taskName} </li>  )
            //console.log(taskOfTheDay)
           }
            days.push(
              <div
                className={`col cell 

                ${
                    taskOfTheDay !== undefined ? "VerticalFlow" : "" 
                }

                ${
                  !dateFns.isSameMonth(day, monthStart)
                    ? "disabled"
                    : dateFns.isSameDay(day, selectedDate) ? "selected" : ""
                }`}
                key={day}
                onClick={(event) =>this.onDateClick(cloneDay)}
              >
                <span className="number">{formattedDate}</span>
                <span className="bg">{formattedDate}</span>
                <div>
                    { taskOfTheDay !== undefined ? <ul className="ListStyle">{renderTasks}</ul> : <div></div>  }
                </div>
              </div>
            );
            day = dateFns.addDays(day, 1);
          }
          rows.push(
            <div className="row" key={day}>
              {days}
            </div>
          );
          days = [];
        }
        return <div className="body">{rows}</div>;
      }

      setReminderDiv = () =>{
          let SelectedDate = this.state.selectedWidgetDate.toISOString()
          //console.log(SelectedDate)
          let tempSc =  this.state.Tasks[SelectedDate];
          //console.log(tempSc)
          let tableData
          if(tempSc !== undefined){
              //console.log("here")
             tableData =  tempSc.map( (x) => 
                  <tr>
                      <td>{x.taskName}</td>
                      <td>{x.location}</td>
                      <td>{x.startTime}</td>
                      <td>{x.endTime}</td>
                      <td>{x.isFree ? "Free" : "Not free"}</td>
                  </tr>
               )
          }
          return(
            <div className="ReminderDiv">
                <BsXCircleFill className="CrossButton"  onClick = {
                    (event) =>{
                        this.setState({
                            ShowSetReminderDiv : false
                        })
                    }

                  } />
                <div className="FormDiv">
                        <p>What do you want to do ? </p>
                        <input type="text" className="formTextInput" onChange={ (event) =>{
                            this.setState({
                                TaskName: event.target.value
                            })
                        }  }/>
                </div>
                <div className="FormDiv">
                    <b>when?</b>
                   <p>{dateFns.format(this.state.selectedWidgetDate,"do MMMM Y")}</p>
                    <b>free time ?</b>
                    <input type="checkbox" onChange = {
                        (event) =>{
                            console.log(event.target.value)
                            if(event.target.value === "on")
                            {
                                //console.log("true")
                                this.setState({
                                    isFreeTime : true
                                })
                            }
                        }
                    }/>
                    <div>
                        <b>Start Time </b>
                    <input type="time" onChange ={
                        (event) =>{
                            this.setState({
                                StartTime : event.target.value
                            })
                        }
                    }/>
                    </div>
                    
                    <div>
                        <b>End Time </b>
                    <input type="time" onChange ={
                        (event) =>{
                            this.setState({
                                EndTime : event.target.value
                            })
                        }
                    }/>
                    </div>

                    
                </div>
                <div className="FormDiv">
                    <b>Location</b>
                    <br></br>
                    <input type="text" className="formTextInput" onChange ={
                         (event) =>{
                            this.setState({
                                TaskLocation : event.target.value
                            })
                        }
                    } />
                </div> 
                <div className="FormDiv">
                    <button onClick={this.CallCreateTask} className="formButton" >Schduele</button>
                </div>
                <div className="TableDiv">
                    <table>
                        <tr>
                            <th>Task</th>
                            <th>Location</th>
                            <th>Start Time</th>
                            <th>End Time</th>
                            <th>Is Free?</th>
                        </tr>
                        { (tempSc !== undefined || tempSc !== null) ? tableData : <tr></tr> }
                    </table>
                </div>

            </div>
          )
      }

      CallCreateTask = (event) =>{
          console.log(this.state.selectedWidgetDate)
            let data = {
                "TaskName" : this.state.TaskName,
                "date" : this.state.selectedWidgetDate.toISOString(),
                "IsFree":this.state.isFreeTime,
                "Location" : this.state.TaskLocation,
                "StartTime" : this.state.StartTime,
                "EndTime" : this.state.EndTime
            }
            this.axios.post(`https://localhost:${extraUtil.PortNumber}/api/task/create`,data,{headers:{
                "Authorization":"Bearer " + tokenUtil.token
                }}).then( (res) =>{
                    console.log("success")
                    this.createTask(data,true)
                } ).catch( (error) =>{
                    console.log("fail")
                    console.log(error)
                } )
                this.setState({
                    ShowSetReminderDiv : false,
                    isFreeTime:false
                })
      }


      render() {
        return (
          <div className="calendar">
            {this.state.ShowSetReminderDiv ?
            <div className="ReminderDivMask">{this.setReminderDiv()}</div>:<div></div>}
            {this.renderHeader()}
            {this.renderDays()}
            {this.renderCells()}
          </div>
        );
      }

    
}

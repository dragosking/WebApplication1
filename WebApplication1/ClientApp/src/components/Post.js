import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { createHashHistory } from 'history'

export const historyr = createHashHistory()

export class Post extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            lat: this.props.location.state.latProps,
            lon: this.props.location.state.lonProps,
            place: this.props.location.state.placeProps,
            yr: this.props.location.state.yrProps,
            smhi: this.props.location.state.smhiProps,
            days: this.props.location.state.daysProps,
            hide: "test2",
            expand:"iconExpand"
        };

    

        
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevProps.location.state !== this.props.location.state) {
            this.setState({
                place: this.props.location.state.placeProps,
                 lat: this.props.location.state.latProps,
                lon: this.props.location.state.lonProps,
                yr: this.props.location.state.yrProps,
                smhi: this.props.location.state.smhiProps,
                days: this.props.location.state.daysProps,
            })
        }

     
    }

    hide = (e) => {
        /*if (this.state.hide == "noshow") {
            this.setState({
                apa: e.target.id,
                hide: "test2",
                expand: "iconCollapse"+e.target.id
            })
        } else {
            this.setState({
                apa: e.target.id,
                hide: "noshow",
                expand: "iconExpand"
            })
        }*/

        /*if (e.target.id == 0) {
            if (this.state.expand == "firstCollapse") {
                this.setState({
                    expand: "firstExpand",
                })
            } else {
                this.setState({
                    expand: "firstCollapse",
                })
            }
        }*/

       
    }


    render() {

        let yrList = this.state.yr.map((Tag, index) => {
            return (
                <table className='test2'>
                    <td className='test7' ><b> {Tag.temperature}</b></td>
                    <td className='test6'> {Tag.time}</td>
            

                </table>

            );
        });

           let daysList = this.state.smhi.map((Tag, index) => {
            return (
                <div>
                   

                    <table className="days">
                        <tr>
                            <td className="days">
                                <h4>{Tag.day}</h4>
                            </td>
                            <td>
                                <div className="iconCollapse" id={index} 
                                 onClick={this.hide}></div>
                            </td>
                        </tr>
                    </table>

                    {
                        Tag.detail.map((SubTag, index2) =>
                            <table className={"firstExpand"}>
                                        <tr >
                                            <td className='first'>{SubTag.hour}</td>
                                            <td className='second'><b>{SubTag.temperatureYR}</b></td>
                                            <td className='third'><b>{SubTag.temperatureSMHI}</b></td>
                                            <td className='fourth'>{SubTag.hour}</td>
                                        </tr>
                                    </table>
                                )
                            }

                        
                </div>
               
            );
        });

       
        return (
            <form>
                {this.state.expand}
                <table className='test2'>
                    <tr>
                        <td className="firstColumn">
                            <h1>{this.state.place}</h1>
                        </td>
                        <td className="secondColumn">
                            <div className='test'></div>
                            <div className='testSMHI'></div>
                        </td>
                        <td className="thirdColumn">
                        
                        </td>
                    </tr>
                </table>

                <div>{daysList}</div>
             
            </form>
           
        );
    }
}

//export default withRouter(Post);
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
                                <button className='iconCollapse' onClick={() => this.hideRow(index)}></button>
                            </td>
                        </tr>
                    </table>

                            {
                                Tag.detail.map((SubTag, index2) =>
                                    <table className='test2'>
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